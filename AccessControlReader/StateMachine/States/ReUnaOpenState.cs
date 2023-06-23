namespace AccessControlReader.StateMachine.States
{
    internal class ReUnaOpenState : State
    {
        public ReUnaOpenState(string details) : base(details) { }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.DoorClosed, typeof(ReadingState));
        }

        public override void OnEnter()
        {
            _rC522?.StopReading();
            _noises?.PlayAsync(NoiseType.SecurityAlarm, this._currentState_CancelationTokenSource);
            _gpio?.RedLightBlink(_currentState_CancelationTokenSource);
            _screen?.Write(DefaultMessage);
            _screen?.StartAnimation(AnimationType.SecurityAlert, this._currentState_CancelationTokenSource, true);
        }

        protected override void OnExit()
        {
            _noises.Stop();
        }
    }
}
