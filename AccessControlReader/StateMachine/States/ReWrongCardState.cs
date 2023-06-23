namespace AccessControlReader.StateMachine.States
{
    internal class ReWrongCardState : State
    {
        public ReWrongCardState(string details) : base(details) { }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.TimeEvent, typeof(ReadingState));
            reactTo.Add(EventType.DoorOpen, typeof(ReUnaOpenState));
        }

        public override void OnEnter()
        {
            _noises?.PlayAsync(NoiseType.NoGo);
            _gpio?.RedLight(_currentState_CancelationTokenSource);
            _screen?.Write(DefaultMessage);
            _screen?.StartAnimation(AnimationType.Hand, this._currentState_CancelationTokenSource, false);
        }
        protected override void OnExit() { }
    }
}
