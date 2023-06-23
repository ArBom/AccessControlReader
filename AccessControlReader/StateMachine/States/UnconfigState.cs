namespace AccessControlReader.StateMachine.States
{
    internal class UnconfigState : State
    {
        public UnconfigState(string details) : base(details) { }

        protected override void AddEvents() 
        {
            reactTo.Add(EventType.MainState_Reading, typeof(ReadingState));
            reactTo.Add(EventType.MainState_Blocked, typeof(BlockedState));
        }

        public override void OnEnter()
        {
            _rC522?.StopReading();
            _screen?.Write(@"   Config me,\n     please");
            _screen?.StartAnimation(AnimationType.Info, _currentState_CancelationTokenSource, true);
            _gpio?.DiodsBlink(this._currentState_CancelationTokenSource);
        }
        protected override void OnExit() { }
    }
}
