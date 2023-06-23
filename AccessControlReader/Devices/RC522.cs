using AccessControlReader.StateMachine;
using Iot.Device.Mfrc522;
using Iot.Device.Rfid;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Spi;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string LastReadValue;

        //bool LastLoopCardPresent;
        readonly System.Timers.Timer readingTimer;

        public NewCardRead newCardRead;
        public static ErrorEvent errorEvent;

        public RC522(XElement Config)
        {
            try
            {
                this.pinReset = int.Parse(Config.Element("ReaderRFIDPinReset").Value);
                this.SpiBusId = int.Parse(Config.Element("ReaderRFIDSpiBusId").Value);
                this.SpiChipSelectLine = int.Parse(Config.Element("ReaderRFIDChipSelectLine").Value);
            }
            catch
            {

            }

            LastReadValue = null;

            readingTimer = new System.Timers.Timer()
            {
                Interval = 500,
                AutoReset = true,
                Enabled = false
            };
            readingTimer.Elapsed += OnTimedEvent;

            SpiConnectionSettings spiConnectionSettings = new(SpiBusId) //0
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
            Console.WriteLine(mfRc522.Version); //TODO
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
                    //TODO critical error
                    Console.WriteLine("mfRc522 is not an object!");
                    return;
                }

            if (mfRc522.IsCardPresent(atqa))
            {
                Console.WriteLine("mfRc522 Card is Present");

                if (mfRc522.ListenToCardIso14443TypeA(out card, new TimeSpan(0, 0, 0, 1)))
                { }
                //TODO zbrejkować jeśli trzeba
                
                byte[] NFC_ID = card.NfcId;

                //remove the "-"signs from NFC_ID
                byte[] new_NFC_ID = NFC_ID.TakeWhile((v, index) => NFC_ID.Skip(index).Any(w => w != '-')).ToArray();

                LastReadValue = BitConverter.ToString(NFC_ID).Replace("-", "");

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
