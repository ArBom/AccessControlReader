namespace AccessControlReader.StateMachine.States
{
    internal class ReToLongTimeOpenState : State
    {
        public ReToLongTimeOpenState(string details) : base(details)
        {
        }

        static internal string DefaultMessage;

        protected override void AddEvents()
        {
            reactTo.Add(EventType.DoorClosed, typeof(ReadingState));
        }

        public override void OnEnter()
        {
            _noises?.PlayAsync(NoiseType.Alert, _currentState_CancelationTokenSource);
            _screen?.DisplayLightBlink(_currentState_CancelationTokenSource);
            _screen?.Write(DefaultMessage);
            _screen?.StartAnimation(AnimationType.OpenAlert, _currentState_CancelationTokenSource, true);
        }
        protected override void OnExit() 
        {
            _noises.Stop();
        }
    }
}