namespace AccessControlReader.StateMachine.States
{
    internal class BlockedState : State
    {
        public BlockedState(string details) : base(details) 
        {
            SQLMessage = details;
        }

        public static string DefaultMessage;
        public static string SQLMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.MainState_Reading, typeof(ReadingState));
            reactTo.Add(EventType.DoorOpen, typeof(BlUnaOpenState));
        }

        public override void OnEnter()
        {
            string ToShow = String.IsNullOrEmpty(SQLMessage) ? DefaultMessage : SQLMessage;

            _rC522?.StopReading();
            _screen?.DisplayLightOff();
            _screen?.Write(ToShow);
            _screen?.StartAnimation(AnimationType.Unactive, this._currentState_CancelationTokenSource, true);
            _gpio?.RedLight(this._currentState_CancelationTokenSource);
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