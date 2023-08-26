using System.Text;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using AccessControlReader.LCD.Animation;
using System.Xml.Linq;
using System.Globalization;

namespace AccessControlReader.Devices
{
    internal sealed class Screen
    {
        readonly private int I2Cdevno;
        readonly private byte I2Caddr;
        readonly private bool Uses8Bit;
        readonly private Lcd1602 lcd;

        /// <summary>
        /// IMPORTANT: Only one task could send data to the screen in one moment
        /// </summary>
        private readonly object send_lock = new();

        /// <summary>
        /// [0] - current text, [1] - previous one
        /// </summary>
        private readonly string[] LastWrites = new string[] { null, null };

        public static ErrorEvent errorEvent;

        //Does the screen show a icon in the left part
        private bool icon = true;

        private readonly Animations animations;

        public Screen(XElement Config)
        {
            if (Config == null)
            {
                errorEvent(GetType(), "Screen: Config is null", 50, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            try
            {
                I2Cdevno = int.Parse(Config.Element("I2CScreenBusId").Value);
                string I2CaddrStr = Config.Element("I2CScreenaddr").Value;
                I2Caddr = byte.Parse(I2CaddrStr, NumberStyles.HexNumber);
                Uses8Bit = bool.Parse(Config.Element("Uses8BitScreen").Value);
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.ToString(), 51, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML });
            }

            I2cDevice screenI2C = I2cDevice.Create(new I2cConnectionSettings(I2Cdevno, I2Caddr));

            byte[] readData = new byte[3];

            try
            {
                screenI2C.WriteRead(new byte[] { 0x00 }, readData);
            }
            catch (Exception ex) 
            {
                errorEvent(GetType(), ex.ToString(), 52, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware });
                lcd = null;
                return;
            }

            this.lcd = new Lcd1602(screenI2C, Uses8Bit);

            //TODO może jakaś diagnostyka czy cos
            lock (send_lock)
            {
                lcd.DisplayOn = true;
                lcd.Clear();
                lcd.BacklightOn = true;
            }

            animations = new Animations(this.lcd, send_lock);

            Write(@"LCD screen:\nOK");
        }

        public bool Icon
        {
            get { return icon; }
            set 
            { 
                if (value)
                {
                    byte[][] image0 = new byte[][]
                               {new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

                    if (lcd is null)
                        return;

                    lock (send_lock)
                    {
                        for (int i = 0; i < 6; i++)
                            lcd.CreateCustomCharacter(i, image0[i]);

                        lcd.SetCursorPosition(0, 0);
                        lcd.Write(new char[] { (char)0, (char)1, (char)2 });

                        lcd.SetCursorPosition(0, 1);
                        lcd.Write(new char[] { (char)3, (char)4, (char)5 });
                    }
                }

                icon = value;
                Write(LastWrites[0]);
            }
        }

        public void Write(string ToWrite)
        {
            if (String.IsNullOrEmpty(ToWrite))
                return;

            //updade the history
            if (LastWrites[0] != ToWrite)
            {
                LastWrites[1] = LastWrites[0];
                LastWrites[0] = ToWrite;
            }

            if (lcd is null)
                return;

            if (icon)
            {
                lock (send_lock)
                {
                    lcd.SetCursorPosition(0, 0);
                    lcd.Write(new String(new char[] { (char)0, (char)1, (char)2 }));
                    lcd.SetCursorPosition(3, 0);
                    lcd.Write("             ");

                    lcd.SetCursorPosition(0, 1);
                    lcd.Write(new String(new char[] { (char)3, (char)4, (char)5 }));

                    lcd.SetCursorPosition(3, 1);
                    lcd.Write("             ");
                }
            }
            else
            {
                lock (send_lock)
                {
                    lcd.Clear();
                }
            }

            string newLineSep = @"\n";
            ToWrite = newLineSep + ToWrite;
            string[] subsLin = ToWrite.Split(newLineSep, StringSplitOptions.RemoveEmptyEntries);

            int lines = subsLin.Length;

            if (lines == 0)
                subsLin = new string[] { ToWrite };

            for(int i = 0; i < subsLin.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(subsLin[i]))
                { 
                    subsLin[i] = "";
                    continue;
                }

                subsLin[i] = subsLin[i].Normalize(NormalizationForm.FormD);
                var subsLinChars = subsLin[i].Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
                subsLin[i] = new string(subsLinChars).Normalize(NormalizationForm.FormC);
            }

            int startPos = icon ? 3 : 0;

            lock (send_lock)
            {
                lcd.SetCursorPosition(startPos, 0);
                lcd.Write(subsLin[0]);
            }

            if (lines > 1)
            {
                lock (send_lock)
                {
                    lcd.SetCursorPosition(startPos, 1);
                    lcd.Write(subsLin[1]);
                }
            }
        }

        public void ShowError(Type senderType, string details, int number, ErrorImportant errorImportant, ErrorTypeIcon[] errorTypes)
        {
            if (lcd is null)
                return;

            string upLine = "Error:" + number.ToString() + " " + senderType.Name;
            string toShow = upLine + $"/n" + details;



            throw new NotImplementedException();
        }

        public void WriteOldText() => Write(LastWrites[1]);

        public void DisplayLightOn()
        {
            if (lcd is null)
                return;

            lock (send_lock)
                lcd.BacklightOn = true;
        }

        public void DisplayLightOff()
        {
            if (lcd is null)
                return;

            lock (send_lock)
                lcd.BacklightOn = false;
        }

        public Task DisplayLightBlink(CancellationTokenSource CancelationObj)
        {
            if (lcd is null)
                return null;

            Task task = new(() =>
            {
                while (!CancelationObj.IsCancellationRequested)
                {
                    DisplayLightOn();
                    Thread.Sleep(1500);

                    if (CancelationObj.IsCancellationRequested)
                        break;

                    DisplayLightOff();
                    Thread.Sleep(500);
                }
            });

            task.Start();

            return task;
        }

        public void StartAnimation(AnimationType animationType, CancellationTokenSource CancelAnimTaken, bool loop, params ErrorTypeIcon[] listOfParams)
        {
            if (lcd is null)
                return;

            if (icon)
                animations.StartAnimation(animationType, CancelAnimTaken, loop, listOfParams);
        }

        ~Screen()
        {
            if (lcd != null)
            {
                lock (send_lock)
                {
                    lcd.DisplayOn = false;
                    lcd.Dispose();
                }
            }
        }
    }
}