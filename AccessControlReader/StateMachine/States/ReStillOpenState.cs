namespace AccessControlReader.StateMachine.States
{
    internal class ReStillOpenState : State
    {
        public ReStillOpenState(string details) : base(details) {  }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.TimeEvent, typeof(ReToLongTimeOpenState));
            reactTo.Add(EventType.DoorClosed, typeof(ReadingState));
        }

        public override void OnEnter()
        {
            _screen?.StartAnimation(AnimationType.Info, _currentState_CancelationTokenSource, true);
            _screen?.Write(DefaultMessage);
            _gpio?.StarttimerOfDoorOpen();
        }
        protected override void OnExit() 
        { 
            _gpio?.DisposeTimers();
        }
    }
}