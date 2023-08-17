using System.Device.Gpio;
using System.Xml.Linq;
using System.Timers;

namespace AccessControlReader.Devices
{
    sealed class GPIO
    {
        public readonly long TimeOfBolt;
        private bool DoorProgrammingOpen = false;
        private readonly long TimeDoorOpenAlert;
        private readonly int PinRedLight;
        private readonly int PinGreenLight;
        private readonly int PinDoorSensor;
        private readonly int PinExitButton;
        private readonly int PinBolt;
        private readonly bool EventTypeExitButton;
        private readonly bool PositiveLogicBolt;
        private readonly bool PositiveLogicDoorOpenSensor;

        public static ErrorEvent errorEvent;
        public StateEvent GPIOStateEvent;
        private readonly GpioController gpioController;

        System.Timers.Timer timerOfDoorOpen;
        System.Timers.Timer timerOfBolt;

        public GPIO(XElement Config)
        {
            gpioController = new GpioController();

            if(Config is null)
            {
                errorEvent(GetType(), "GPIO: Config is null", 30, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            try
            {
                PinRedLight = int.Parse(Config.Element("PinRedLight").Value);
                PinGreenLight = int.Parse(Config.Element("PinGreenLight").Value);
                PinDoorSensor = int.Parse(Config.Element("PinDoorSensor").Value);
                PinBolt = int.Parse(Config.Element("PinBolt").Value);
                PinExitButton = int.Parse(Config.Element("PinExitButton").Value);
                EventTypeExitButton = bool.Parse(Config.Element("EventTypeExitButton").Value);

                TimeOfBolt = int.Parse(Config.Element("TimeOfBolt").Value);
                TimeDoorOpenAlert = int.Parse(Config.Element("TimeDoorOpenAlert").Value);

                PositiveLogicBolt = bool.Parse(Config.Element("PositiveLogicBolt").Value);
                PositiveLogicDoorOpenSensor = bool.Parse(Config.Element("PositiveLogicDoorSensor").Value);
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.Message, 31, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            if (TimeOfBolt < 1000)
            {
                //new time of Bolt -> 5s
                TimeOfBolt = 5000;
                errorEvent(GetType(), "Time of bolt open cannot be shorter than 1s", 78, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            if (TimeDoorOpenAlert <= 500)
            {
                //new time of Bolt -> 10s
                TimeDoorOpenAlert = 10000;
                errorEvent(GetType(), "Time alert cannot be shorter than .5s", 79, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            try
            {
                gpioController.OpenPin(PinExitButton, PinMode.Input);
                gpioController.OpenPin(PinBolt, PinMode.Output);
                gpioController.OpenPin(PinRedLight, PinMode.Output, PinValue.Low);
                gpioController.OpenPin(PinGreenLight, PinMode.Output, PinValue.Low);
                gpioController.OpenPin(PinDoorSensor, PinMode.Input);
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.Message, 35, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
            }

            try
            {
                if (PositiveLogicBolt)
                    gpioController.Write(PinBolt, PinValue.Low);            
                else
                    gpioController.Write(PinBolt, PinValue.High);
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.Message, 34, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
            }

            if (gpioController.IsPinOpen(PinDoorSensor))
                gpioController.RegisterCallbackForPinValueChangedEvent(PinDoorSensor, PinEventTypes.Falling | PinEventTypes.Rising, OnDoorSensorEvent);
            else
                errorEvent(GetType(), "Door Sensor pin is closed", 37, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });

            if (gpioController.IsPinOpen(PinExitButton))
            {
                if (EventTypeExitButton)
                    gpioController.RegisterCallbackForPinValueChangedEvent(PinExitButton, PinEventTypes.Rising, OnExitButtonPress);
                else
                    gpioController.RegisterCallbackForPinValueChangedEvent(PinExitButton, PinEventTypes.Falling, OnExitButtonPress);
            }
            else
                errorEvent(GetType(), "Exit Button pin is closed", 33, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
        }

        private void OpenDoorBolt()
        {
            DoorProgrammingOpen = true;

            if (!gpioController.IsPinOpen(PinBolt))
            {
                errorEvent(GetType(), "Bolt pin is closed", 32, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                return;
            }

            if (PositiveLogicBolt)
                gpioController.Write(PinBolt, PinValue.High);
            else
                gpioController.Write(PinBolt, PinValue.Low);
        }

        private void StopDoorBolt()
        {
            if (gpioController.IsPinOpen(PinBolt))
            {
                Thread.Sleep(1000);

                if (PositiveLogicBolt)
                    gpioController.Write(PinBolt, PinValue.Low);
                else
                    gpioController.Write(PinBolt, PinValue.High);

                Thread.Sleep(500); //Time to return of electro-bolt;
            }
            else
            {
                errorEvent(GetType(), "Bolt pin is closed", 36, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
            }

            DoorProgrammingOpen = false;
        }

        private void OnExitButtonPress(object sender, PinValueChangedEventArgs args)
        {
            OpenDoorBolt();
            GPIOStateEvent?.Invoke(EventType.OpenButton, null);
        }

        public void StarttimerOfDoorOpen()
        {
            Console.WriteLine("timerOfDoorOpen.Interval: " + TimeDoorOpenAlert);
            timerOfDoorOpen = new System.Timers.Timer()
            {
                Interval = TimeDoorOpenAlert,
                AutoReset = false,
            };
            timerOfDoorOpen.Elapsed += DoorOpenTooLong;
            timerOfDoorOpen.Start();
        }

        private void OnDoorSensorEvent(object sender, PinValueChangedEventArgs args)
        {
            const string Open = "OPEN";
            const string Close = "CLOSE";
           
            if ((args.ChangeType is PinEventTypes.Falling && !PositiveLogicDoorOpenSensor) || (args.ChangeType is PinEventTypes.Rising && PositiveLogicDoorOpenSensor))
            {
                Console.WriteLine(Open);

                if (DoorProgrammingOpen)
                {
                    StopDoorBolt();
                    GPIOStateEvent?.Invoke(EventType.DoorOpen, "ProgrammingOpen");

                    return;
                }

                GPIOStateEvent?.Invoke(EventType.DoorOpen, "UnautOpen");
            }
            else
            {
                Console.WriteLine(Close);

                if (timerOfDoorOpen != null)
                {
                    timerOfDoorOpen.Elapsed -= DoorOpenTooLong;
                    timerOfDoorOpen.Stop();
                    timerOfDoorOpen.Dispose();
                    timerOfDoorOpen = null;
                }

                GPIOStateEvent?.Invoke(EventType.DoorClosed, null);
            }
        }

        private void DoorOpenTooLong(Object source, ElapsedEventArgs e) =>  GPIOStateEvent?.Invoke(EventType.TimeEvent, "DoorOpenTooLong");

        public Task DiodsBlink(object CancelationObj)
        {
            if (!gpioController.IsPinOpen(PinGreenLight) || !gpioController.IsPinOpen(PinRedLight))
            {
                errorEvent(GetType(), "Diod pin(s) closed", 75, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                return null;
            }

            Task task = new(() =>
            {
                CancellationToken DiodsBlinkToken = (CancellationToken)CancelationObj;

                bool state = false;

                while (!DiodsBlinkToken.IsCancellationRequested)
                {
                    if (state)
                    {
                        gpioController.Write(PinGreenLight, PinValue.Low);
                        gpioController.Write(PinRedLight, PinValue.High);
                    }
                    else
                    {
                        gpioController.Write(PinGreenLight, PinValue.High);
                        gpioController.Write(PinRedLight, PinValue.Low);
                    }

                    Thread.Sleep(250);
                    state = !state;
                }

                gpioController.Write(PinRedLight, PinValue.Low);
                gpioController.Write(PinGreenLight, PinValue.Low);
            });

            task.Start();
            return task;
        }

        public Task GreenLight(object CancelationObj)
        {
            if (!gpioController.IsPinOpen(PinGreenLight))
            {
                errorEvent(GetType(), "Green diod pin closed", 74, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                return null;
            }

            Task task = new(() =>
            {
                CancellationToken DiodsBlinkToken = (CancellationToken)CancelationObj;
                gpioController.Write(PinGreenLight, PinValue.High);

                while (!DiodsBlinkToken.IsCancellationRequested)
                {
                    Thread.Sleep(100);
                }

                gpioController.Write(PinGreenLight, PinValue.Low);
            });

            task.Start();
            return task;
        }

        public Task RedLight(CancellationTokenSource CancelationObj)
        {
            if (!gpioController.IsPinOpen(PinRedLight))
            {
                errorEvent(GetType(), "Red diod pin closed", 77, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                return null;
            }

            Task task = new(() =>
            {
                gpioController.Write(PinRedLight, PinValue.High);

                while (!CancelationObj.IsCancellationRequested)
                {
                    Thread.Sleep(100);
                }

                gpioController.Write(PinRedLight, PinValue.Low);
            });

            task.Start();
            return task;
        }

        public Task RedLightBlink(CancellationTokenSource CancelationObj)
        {
            if (!gpioController.IsPinOpen(PinRedLight))
            {
                errorEvent(GetType(), "Red diod pin closed", 77, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                return null;
            }

            Task task = new(() =>
            {
                while (!CancelationObj.IsCancellationRequested)
                {
                    gpioController.Write(PinRedLight, PinValue.High);
                    Thread.Sleep(400);

                    if (CancelationObj.IsCancellationRequested)
                        break;

                    gpioController.Write(PinRedLight, PinValue.Low);
                    Thread.Sleep(200);
                }

                gpioController.Write(PinRedLight, PinValue.Low);
            });

            task.Start();
            return task;
        }

        public Task OpenDoorBolt(CancellationTokenSource CancelationObj)
        {
            bool Stop = false;

            timerOfBolt = new System.Timers.Timer()
            {
                Interval = TimeOfBolt,
            };
            timerOfBolt.Elapsed += (sender, args) => { Stop = true; };
            timerOfBolt.Elapsed += (sender, args) => GPIOStateEvent?.Invoke(EventType.TimeEvent, null);

            CancellationTokenSource BoltToken;

            if (CancelationObj != null)
                BoltToken = CancelationObj;
            else
                BoltToken = new CancellationTokenSource((int)(TimeOfBolt + 200));

            timerOfBolt.Start();
            OpenDoorBolt();

            Task task = new(() =>
            {
                while (!BoltToken.IsCancellationRequested && !Stop)
                {
                    Thread.Sleep(100);
                }
                StopDoorBolt();
            });

            task.Start();
            return task;
        }

        public void DisposeTimers()
        {
            if (timerOfDoorOpen != null)
            {
                timerOfDoorOpen.Stop();
                timerOfDoorOpen.Elapsed -= DoorOpenTooLong;
                timerOfDoorOpen.Dispose();
                timerOfDoorOpen = null;
            }

            if (timerOfBolt != null)
            {
                timerOfBolt.Stop();
                timerOfBolt.Dispose();
                timerOfBolt = null;
            }
        }

        ~GPIO()
        {
            this.DisposeTimers();

            //Close pins
            gpioController.ClosePin(PinRedLight);
            gpioController.ClosePin(PinGreenLight);
            gpioController.ClosePin(PinBolt);
            gpioController.ClosePin(PinDoorSensor);
            gpioController.ClosePin(PinExitButton);
        }
    }
}
