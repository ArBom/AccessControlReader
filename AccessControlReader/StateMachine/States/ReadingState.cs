using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlReader.StateMachine.States
{
    internal class ReadingState : State
    {
        public ReadingState(string details) : base(details) 
        {
            SQLMessage = details;
        }

        public static string DefaultMessage;
        public static string SQLMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.MainState_Blocked, typeof(BlockedState));
            reactTo.Add(EventType.CorrectCard, typeof(ReOpenRFIDState));
            reactTo.Add(EventType.OpenButton, typeof(ReOpenButtonState));
            reactTo.Add(EventType.WrongCard, typeof(ReWrongCardState));
            reactTo.Add(EventType.DoorOpen, typeof(ReUnaOpenState));
        }

        public override void OnEnter()
        {
            string ToShow = String.IsNullOrEmpty(SQLMessage) ? DefaultMessage : SQLMessage;

            _rC522?.StartReading();
            _screen?.DisplayLightOn();
            _screen?.Write(ToShow);
            _screen?.StartAnimation(AnimationType.RFID, this._currentState_CancelationTokenSource, true);
        }

        public static void MessageUpdate(string NewMessage)
        {
            SQLMessage = NewMessage;
            string ToShow = String.IsNullOrEmpty(NewMessage) ? DefaultMessage : NewMessage;
            _screen?.Write(ToShow);
        }

        protected override void OnExit() { }
    }
}
