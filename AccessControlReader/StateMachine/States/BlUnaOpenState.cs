namespace AccessControlReader.StateMachine.States
{
    internal class BlUnaOpenState : State
    {
        public BlUnaOpenState(string details) : base(details) { }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.DoorClosed, typeof(BlockedState));
        }
        public override void OnEnter()
        {
            var sb = _screen?.DisplayLightBlink(_currentState_CancelationTokenSource);
            _noises?.PlayAsync(NoiseType.SecurityAlarm, _currentState_CancelationTokenSource);
            var rlb = _gpio?.RedLightBlink(_currentState_CancelationTokenSource);
            _screen?.Write(DefaultMessage);
            _screen?.StartAnimation(AnimationType.SecurityAlert, _currentState_CancelationTokenSource, true);

            StateTasks.Add(sb);
            StateTasks.Add(rlb);
        }

        protected override void OnExit() 
        {
            _noises.Stop();
        }
    }
}
