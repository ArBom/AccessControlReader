using Iot.Device.Mfrc522;
using Iot.Device.Rfid;
using System.Device.Gpio;
using System.Device.Spi;
using System.Timers;
using System.Xml.Linq;

namespace AccessControlReader.Devices
{
    internal class RC522
    {
        readonly int pinReset;
        readonly int SpiBusId;
        readonly int SpiChipSelectLine;

        readonly MfRc522 mfRc522;
        Data106kbpsTypeA card = null;
        //string LastReadValue;

        //bool LastLoopCardPresent;
        readonly System.Timers.Timer readingTimer;

        public NewCardRead newCardRead;
        public static ErrorEvent errorEvent;

        public RC522(XElement Config)
        {
            if (Config is null)
            {
                errorEvent(GetType(), "RC522: Config is null", 40, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML, ErrorTypeIcon.RFID });
            }
            try
            {
                this.pinReset = int.Parse(Config.Element("ReaderRFIDPinReset").Value);
                this.SpiBusId = int.Parse(Config.Element("ReaderRFIDSpiBusId").Value);
                this.SpiChipSelectLine = int.Parse(Config.Element("ReaderRFIDChipSelectLine").Value);
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.Message, 41, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.XML, ErrorTypeIcon.RFID });
            }

            readingTimer = new System.Timers.Timer()
            {
                Interval = 500,
                AutoReset = true,
                Enabled = false
            };
            readingTimer.Elapsed += OnTimedEvent;

            SpiConnectionSettings spiConnectionSettings = new(SpiBusId)
            {
                ChipSelectLine = SpiChipSelectLine,               

                // Set clock to 5MHz
                ClockFrequency = 5_000_000
            };

            SpiDevice spiDevice = SpiDevice.Create(spiConnectionSettings);
            GpioController gpioController = new();
            bool shouldDispose = false;

            this.mfRc522 = new MfRc522(spiDevice, pinReset, gpioController, shouldDispose);
        }

        public void StartReading()
        {
            mfRc522.Enabled = true;
            readingTimer.Start();
            if (mfRc522.Version != new Version(1,0) && mfRc522.Version != new Version(2,0))
            {
                errorEvent(GetType(), "Unknow reader version", 42, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.RFID });
            }
        }

        public void StopReading()
        {
            readingTimer.Stop();
            mfRc522.Halt();
            mfRc522.PrepareForSleep();
            mfRc522.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Odczytuję");
            byte[] atqa = new byte[2];

            if (this.mfRc522 is null)
                {
                errorEvent(GetType(), "mfRc522 is not an object", 43, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Hardware, ErrorTypeIcon.RFID });
                Console.WriteLine("mfRc522 is not an object!");
                return;
                }

            if (mfRc522.IsCardPresent(atqa))
            {
                Console.WriteLine("mfRc522 Card is Present");

                if (mfRc522.ListenToCardIso14443TypeA(out card, new TimeSpan(0, 0, 0, 1)))
                { }
                else
                {
                    Console.WriteLine("Its not a Type-A card");
                    return;
                }
                //TODO zbrejkować jeśli trzeba
                
                byte[] NFC_ID = card.NfcId;

                //remove the "-"signs from NFC_ID
                byte[] new_NFC_ID = NFC_ID.TakeWhile((v, index) => NFC_ID.Skip(index).Any(w => w != '-')).ToArray();

                //convert to Uint
                UInt32 Uint_NFC_ID = BitConverter.ToUInt32(new_NFC_ID, 0);
                Console.WriteLine("Odczyt UInt32: " + Uint_NFC_ID.ToString());
                newCardRead?.Invoke(Uint_NFC_ID);
            }
            else 
            { 
                //LastLoopCardPresent = false;
            }
        }

        ~RC522()
        {
            StopReading();
            mfRc522.Dispose();
        }
    }
}
