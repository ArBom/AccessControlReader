﻿using AccessControlReader.Devices;
using AccessControlReader.StateMachine.States;
using Iot.Device.Mcp23xxx;
using System.Net.NetworkInformation;
using System.Timers;

namespace AccessControlReader.StateMachine
{
    internal sealed class StateMachine
    {
        private State ActualState;
        private readonly System.Timers.Timer CheckingMainStateTimer;
        private readonly System.Timers.Timer ExitFromWrongCardStateTimer;
        private Nullable<Error> CurrentError;

        private GPIO _gpio;
        public GPIO gpio
        {
            set
            {
                if (_gpio != null)
                    _gpio.GPIOStateEvent -= EnterNewState;

                State._gpio = value;
                this._gpio = value;
                _gpio.GPIOStateEvent += EnterNewState;
            }
        }

        public RC522 rC5221
        {
            set 
            {
                if (_accessControlDb != null && rC5221 != null)
                    rC5221.newCardRead -= _accessControlDb.CheckCard;

                State._rC522 = value;

                if (_accessControlDb != null)
                {
                    rC5221.newCardRead += _accessControlDb.CheckCard;
                }
            }

            private get { return State._rC522; }
        }

        public Screen screen
        {
            set { State._screen = value; }
            private get { return State._screen; }
        }


        public Noises noises
        {
            set
            {
                State._noises = value;
            }
        }

        private AccessControlDb _accessControlDb;
        public AccessControlDb accessControlDb
        {
            set
            {
                if (_accessControlDb != null)
                {
                    _accessControlDb.DataBaseEvent -= EnterNewState;
                }

                if (_accessControlDb != null && rC5221 != null)
                    rC5221.newCardRead -= _accessControlDb.CheckCard;

                _accessControlDb = value;

                _accessControlDb.DataBaseEvent += EnterNewState;

                if (rC5221 != null)
                {
                    rC5221.newCardRead += _accessControlDb.CheckCard;
                }
            }
        }

        public StateMachine() 
        { 
            ActualState = new InitState(null);
            CurrentError = null;

            GPIO.errorEvent += ErrorHappens;
            Noises.errorEvent += ErrorHappens;
            RC522.errorEvent += ErrorHappens;
            Screen.errorEvent += ErrorHappens;
            Configurator.errorEvent += ErrorHappens;
            State.errorEvent += ErrorHappens;
            AccessControlDb.errorEvent += ErrorHappens;

            ExitFromWrongCardStateTimer = new System.Timers.Timer()
            {
                Interval = 4000,
                AutoReset = false,
            };
            ExitFromWrongCardStateTimer.Elapsed += (sender, args) => EnterNewState(EventType.TimeEvent, null);

            CheckingMainStateTimer = new System.Timers.Timer()
            {
                //Check it once a minut
                Interval = 60000,
                AutoReset = true,
            };
            CheckingMainStateTimer.Elapsed += CyclicCheckMainState;
            CheckingMainStateTimer.Start();
        }

        public void EnterNewState(EventType eventType, string details)
        {
            State newState = ActualState.EnterNewState(eventType, details);

            //Check whether change of state is possible
            if (newState is null)
            {
                //Check whether update main sreen message
                if (eventType == EventType.MainState_Reading)
                {
                    //Updating screen message for Reading-State
                    ReadingState.MessageUpdate(details);
                }

                if (eventType == EventType.MainState_Blocked)
                { 
                    //Updating screen message for Blocked-State
                    BlockedState.MessageUpdate(details);
                }

                return;
            }

            //Check the necessery state change or update
            if (newState.GetType() == ActualState.GetType())
                return;

            //Staring the timer of wrong card state 
            if (newState.GetType() == typeof(ReWrongCardState))
                ExitFromWrongCardStateTimer.Start();

            //Stop the timer of wrong card state 
            if (ActualState.GetType() == typeof(ReWrongCardState))
                ExitFromWrongCardStateTimer.Stop();

            ActualState.Dispose();
            ActualState = newState;
            newState.OnEnter();
        }

        private void ErrorHappens(Type senderType, string details, int number, ErrorImportant errorImportant, ErrorTypeIcon[] errorTypes)
        {
            //Currently its with error
            if (CurrentError.HasValue)
            {
                //new error is less important than current
                if (((int)errorImportant) < ((int)CurrentError.Value.errorImportant))
                    //ignore new one
                    return;

                //new error is as important as current...
                if (((int)errorImportant) == ((int)CurrentError.Value.errorImportant) &&
                    //...but is in less relevant part of program
                    number > CurrentError.Value.ErrorNum)
                    //ignore new one
                    return;
            }

            switch (errorImportant) 
            {
                case ErrorImportant.Critical:
                    {

                    }
                    break;

                case ErrorImportant.Warning:
                    {

                    }
                    break;

                case ErrorImportant.Info:
                    {

                    }
                    break;
            }
            ////////
            //TODO//
            ////////
        }

        private void UpdateInitStateData()
        {
            if (ActualState is not InitState initState)
                return;

            var NetwInt = from NI in NetworkInterface.GetAllNetworkInterfaces()
                          where (NI.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                                 NI.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                          select NI;

            if (!NetwInt.Any())
            {
                ErrorHappens(this.GetType(), "No net. card", 3, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.LAN, ErrorTypeIcon.Hardware });
                return;
            }

            var NetwIntUp = from NI in NetwInt
                            where NI.OperationalStatus == OperationalStatus.Up
                            select NI;

            if (!NetwIntUp.Any())
            {
                ErrorHappens(this.GetType(), "No working net. card", 4, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.LAN });
                return;
            }

            string macAddr;
            try
            {
                macAddr = NetwIntUp.First().GetPhysicalAddress().ToString();
            }
            catch
            {
                ErrorHappens(GetType(), "No MAC addr.", 5, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.LAN, ErrorTypeIcon.Hardware });
                return;
            }
            initState.macAddr = macAddr;

            try
            {
                initState.ipAddr = NetwInt?.First().GetIPProperties().UnicastAddresses.First().Address.ToString();
            }
            catch
            {
                ErrorHappens(GetType(), "No IP addr.", 6, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.LAN });
                return;
            }           

            ReaderData? newSQLdata = _accessControlDb?.UpdateReader(macAddr);
        }

        private void CyclicCheckMainState(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("sprawdzam czytnik w sql");
            ReaderData? newSQLdata;

            if (ActualState is InitState initState)
            {
                string macAddr = ((InitState)ActualState).macAddr;
                newSQLdata = _accessControlDb?.UpdateReader(macAddr);

                if (newSQLdata is null)
                    return;

                //Is it configurated
                if (newSQLdata.Value.ErrorNo == 1)
                {
                    EnterNewState(EventType.MainState_Unconfig, "");
                    return;
                }
            }
            else
            {
                newSQLdata = _accessControlDb?.UpdateReader(); //todo this reader bywa nullem

                if (newSQLdata is null)
                    return;

                Console.WriteLine("State Machine newSQLdata is not null");
            }

            //Set the message on screen with SQL
            BlockedState.SQLMessage = newSQLdata.Value.ToShow;
            ReadingState.SQLMessage = newSQLdata.Value.ToShow;

            if (newSQLdata.Value.IsActive)
                //Reader is configurated & active
                EnterNewState(EventType.MainState_Reading, newSQLdata.Value.ToShow);
            else
                //Reader is configurated but unactive
                EnterNewState(EventType.MainState_Blocked, newSQLdata.Value.ToShow);
        }
    }
}