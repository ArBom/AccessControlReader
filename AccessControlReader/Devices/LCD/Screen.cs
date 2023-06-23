using System.Text;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using AccessControlReader.LCD.Animation;
using System.Xml.Linq;
using System.Globalization;

namespace AccessControlReader.Devices
{
    internal class Screen
    {
        readonly private int I2Cdevno;
        readonly private byte I2Caddr;
        readonly private bool Uses8Bit;
        readonly private Lcd1602 lcd;

        private object send_lock = new();

        public static ErrorEvent errorEvent;

        private bool icon = true;
        public bool Icon
        {
            get { return icon; }
            set 
            { 
                if (value)
                {
                  byte[][] image0 = new byte[][]
                  { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

                    lock (send_lock)
                    {
                        for (int i = 0; i < 6; i++)
                            lcd.CreateCustomCharacter(i, image0[i]);

                        lcd.SetCursorPosition(0, 0);
                        lcd.Write(new String(new char[] { (char)0 }));
                        lcd.Write(new String(new char[] { (char)1 }));
                        lcd.Write(new String(new char[] { (char)2 }));

                        lcd.SetCursorPosition(0, 1);
                        lcd.Write(new String(new char[] { (char)3 }));
                        lcd.Write(new String(new char[] { (char)4 }));
                        lcd.Write(new String(new char[] { (char)5 }));
                    }
                }

                icon = value; 
            }
        }

        public void ErrorMess(int ErrorNumber)
        { 
            throw new NotImplementedException();
        }

        public void Write(string ToWrite)
        {
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
                    lcd.SetCursorPosition(0, 0);
                }
            }
            if (String.IsNullOrEmpty(ToWrite))
                return;
           
            
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

        public Screen(XElement Config)
        {
            if (Config == null)
            {
                //TODO
            }

            try
            {
                I2Cdevno = int.Parse(Config.Element("I2CScreenBusId").Value);
                string r = Config.Element("I2CScreenaddr").Value;
                I2Caddr = byte.Parse(r, System.Globalization.NumberStyles.HexNumber);
                Uses8Bit = bool.Parse(Config.Element("Uses8BitScreen").Value);
            }
            catch (System.Exception e)
            {
                string er = e.ToString();
            }

            I2cDevice screenI2C = I2cDevice.Create(new I2cConnectionSettings(I2Cdevno, I2Caddr));
            this.lcd = new Lcd1602(screenI2C, Uses8Bit);

            //TODO jakaś diagnostyka czy cos
            lock (send_lock)
            {
                lcd.DisplayOn = true;
                lcd.Clear();
                lcd.BacklightOn = true;
            }

            Write(@"LCD screen:\nOK");
        }

        public void DisplayLightOn()
        {
            lock (send_lock)
                this.lcd.BacklightOn = true;
        }

        public void DisplayLightOff()
        {
            lock (send_lock)
                this.lcd.BacklightOn = false;
        }

        public Task DisplayLightBlink(CancellationTokenSource CancelationObj)
        {
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

        ~Screen()
        {
            if (this.lcd != null)
            {
                lock (send_lock)
                {
                    this.lcd.DisplayOn = false;
                    this.lcd.Dispose();
                }
            }
        }

        public void StartAnimation(AnimationType animationType, CancellationTokenSource CancelAnimTaken, bool loop)
        {
            if (icon)
                Animations.StartAnimation(this.lcd, animationType, ref send_lock, CancelAnimTaken, loop);
        }
    }
}
