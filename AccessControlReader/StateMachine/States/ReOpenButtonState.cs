namespace AccessControlReader.StateMachine.States
{
    internal class ReOpenButtonState : State
    {
        public ReOpenButtonState(string details) : base(details) { }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.TimeEvent, typeof(ReadingState));
            reactTo.Add(EventType.DoorOpen, typeof(ReStillOpenState));
        }

        public override void OnEnter()
        {
            Task bolt = _gpio?.OpenDoorBolt(_currentState_CancelationTokenSource);
            StateTasks.Add(bolt);

            _gpio?.StarttimerOfDoorOpen();
            _screen?.Write(DefaultMessage);
            _screen?.StartAnimation(AnimationType.Info, this._currentState_CancelationTokenSource, false);
        }
        protected override void OnExit() 
        {
            _gpio.DisposeTimers();
        }
    }
}