namespace AccessControlReader.StateMachine.States
{
    internal class ReOpenRFIDState : State
    {
        public ReOpenRFIDState(string details) : base(details) { }

        static internal string DefaultMessage;

        protected override void AddEvents() 
        {
            reactTo.Add(EventType.TimeEvent, typeof(ReadingState));
            reactTo.Add(EventType.DoorOpen, typeof(ReStillOpenState));
        }

        public override void OnEnter()
        {
            _noises?.PlayAsync(NoiseType.Open);
            
            Task light = _gpio?.GreenLight(_currentState_CancelationTokenSource);
            StateTasks.Add(light);

            Task bolt = _gpio?.OpenDoorBolt(_currentState_CancelationTokenSource);
            StateTasks.Add(bolt);

            _screen?.Write(DefaultMessage + @"\n" + details);
            _screen?.StartAnimation(AnimationType.Open, this._currentState_CancelationTokenSource, true);

        }
        protected override void OnExit() 
        {
            _gpio.DisposeTimers();
        }
    }
}