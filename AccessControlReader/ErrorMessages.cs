using AccessControlReader.Devices;
using AccessControlReader.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlReader
{
    internal class ErrorMessages
    {
        public AccessControlDb _accessControlDb;
        public Screen screen;
        public Noises noises;

        private Error? CurrentError;

        public ErrorMessages() 
        {
            GPIO.errorEvent += ErrorHappens;
            Noises.errorEvent += ErrorHappens;
            RC522.errorEvent += ErrorHappens;
            Screen.errorEvent += ErrorHappens;
            Configurator.errorEvent += ErrorHappens;
            State.errorEvent += ErrorHappens;
            AccessControlDb.errorEvent += ErrorHappens;
            StateMachine.StateMachine.errorEvent += ErrorHappens;
        }

        private void ErrorHappens(Type senderType, string details, int number, ErrorImportant errorImportant, ErrorTypeIcon[] errorTypes)
        {
            var now = DateTime.Now.TimeOfDay.ToString();

            //Example bellow
            //                     13:21:37 -                      critical     issue №    69         of     Noises             :     it woudnt to play     
            string ConsoleMessage = now + " - " + errorImportant.ToString() + " issue №" + number + " of " + senderType.Name + ": " + details;

            //show message at app console
            switch (errorImportant)
            {
                case ErrorImportant.Critical:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ConsoleMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;

                case ErrorImportant.Warning:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ConsoleMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;

                case ErrorImportant.Info:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(ConsoleMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;
            }

            //Currently its with error
            if (CurrentError.HasValue)
            {
                //new error is less important than current
                if (((int)errorImportant) < ((int)CurrentError.Value.errorImportant))
                    //ignore new one
                    return;

                //new error is as important as current...
                if (((int)errorImportant) == ((int)CurrentError.Value.errorImportant) &&
                    //...but is from less relevant part of program
                    number > CurrentError.Value.ErrorNum)
                    //ignore new one
                    return;
            }

            //Remember new error
            CurrentError = new Error(errorImportant, number);

            //Send error message to DB
            _accessControlDb?.SendErrorInfoAsync(number, details);

            //Show icons and text on the screen
            screen?.ShowError(senderType, details, number, errorImportant, errorTypes);

            //Play noise of error
            noises?.PlayAsync(NoiseType.Error);
        }
    }
}
