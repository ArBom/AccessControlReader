using AccessControlReader.Devices;
using AccessControlReader.StateMachine.States;
using System.Timers;

namespace AccessControlReader.StateMachine
{
    internal sealed class StateMachine
    {
        private State ActualState;
        private readonly System.Timers.Timer CheckingMainStateTimer;
        private readonly System.Timers.Timer ExitFromWrongCardStateTimer;

        private GPIO _gpio;
        public GPIO gpio
        {
            set
            {
                if (_gpio != null)
                    _gpio.GPIOStateEvent -= EnterNewState;

                State._gpio = value;
                this._gpio = value;
                _gpio.GPIOStateEvent += EnterNewState;
            }
        }

        public RC522 rC5221
        {
            set 
            {
                if (_accessControlDb != null && rC5221 != null)
                    rC5221.newCardRead -= _accessControlDb.CheckCard;

                State._rC522 = value;

                if (_accessControlDb != null)
                {
                    rC5221.newCardRead += _accessControlDb.CheckCard;
                }
            }

            private get { return State._rC522; }
        }

        public Screen screen
        {
            set { State._screen = value; }
            private get { return State._screen; }
        }


        public Noises noises
        {
            set
            {
                State._noises = value;
            }
        }

        private AccessControlDb _accessControlDb;
        public AccessControlDb accessControlDb
        {
            set
            {
                if (_accessControlDb != null)
                    _accessControlDb.DataBaseEvent -= EnterNewState;

                if (_accessControlDb != null && rC5221 != null)
                    rC5221.newCardRead -= _accessControlDb.CheckCard;

                _accessControlDb = value;

                _accessControlDb.DataBaseEvent += EnterNewState;

                if (rC5221 != null)
                {
                    rC5221.newCardRead += _accessControlDb.CheckCard;
                }
            }
        }

        public StateMachine() 
        { 
            ActualState = new InitState(null);

            GPIO.errorEvent += ErrorHappens;
            Noises.errorEvent += ErrorHappens;
            RC522.errorEvent += ErrorHappens;
            Screen.errorEvent += ErrorHappens;
            Configurator.errorEvent += ErrorHappens;

            ExitFromWrongCardStateTimer = new System.Timers.Timer()
            {
                Interval = 4000,
                AutoReset = false,
            };
            ExitFromWrongCardStateTimer.Elapsed += (sender, args) => EnterNewState(EventType.TimeEvent, null);

            CheckingMainStateTimer = new System.Timers.Timer()
            {
                //Check it once a minut
                Interval = 20000,
                AutoReset = true,
            };
            CheckingMainStateTimer.Elapsed += CyclicCheckMainState;
            CheckingMainStateTimer.Start();
        }

        public void EnterNewState(EventType eventType, string details)
        {
            State newState = ActualState.EnterNewState(eventType, details);

            //Check whether change of state is possible
            if (newState is null)
            {
                //Check whether update main sreen message
                if (eventType == EventType.MainState_Reading)
                {
                    //Updating screen message for Reading-State
                    ReadingState.MessageUpdate(details);
                }

                if (eventType == EventType.MainState_Blocked)
                { 
                    //Updating screen message for Blocked-State
                    BlockedState.MessageUpdate(details);
                }

                return;
            }

            //Check the necessery state change or update
            if (newState.GetType() == ActualState.GetType())
                return;

            //Staring the timer of wrong card state 
            if (newState.GetType() == typeof(ReWrongCardState))
                ExitFromWrongCardStateTimer.Start();

            //Stop the timer of wrong card state 
            if (ActualState.GetType() == typeof(ReWrongCardState))
                ExitFromWrongCardStateTimer.Stop();

            ActualState.Dispose();
            ActualState = newState;
            newState.OnEnter();
        }

        private void ErrorHappens(Type senderType, string details, int number, object[] errorTypes)
        {
            ////////
            //TODO//
            ////////
        }

        private void CyclicCheckMainState(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("sprawdzam czytnik w sql");
            if (ActualState.GetType() == typeof(InitState))
            {
                string macAddr = ((InitState)ActualState).macAddr;
                ReaderData? newSQLdata = _accessControlDb?.UpdateReader(macAddr);

                if (newSQLdata is null)
                    return;

                //Is it configurated
                if (newSQLdata.Value.ErrorNo == 1)
                {
                    EnterNewState(EventType.MainState_Unconfig, "");
                    return;
                }

                BlockedState.SQLMessage = newSQLdata.Value.ToShow;
                ReadingState.SQLMessage = newSQLdata.Value.ToShow;



                if (newSQLdata.Value.IsActive)
                    //Reader is configurated & active
                    EnterNewState(EventType.MainState_Reading, newSQLdata.Value.ToShow);
                else
                    //Reader is configurated but unactive
                    EnterNewState(EventType.MainState_Blocked, newSQLdata.Value.ToShow);

            }
            else
            {
                ReaderData? newSQLdata = _accessControlDb?.UpdateReader(); //this reader bywa nullem

                if (newSQLdata is null)
                    return;

                Console.WriteLine("State Machine newSQLdata is not null");

                BlockedState.SQLMessage = newSQLdata.Value.ToShow;
                ReadingState.SQLMessage = newSQLdata.Value.ToShow;

                if (newSQLdata.Value.IsActive)
                    //Reader is configurated & active
                    EnterNewState(EventType.MainState_Reading, newSQLdata.Value.ToShow);
                else
                    //Reader is configurated but unactive
                    EnterNewState(EventType.MainState_Blocked, newSQLdata.Value.ToShow);
            }
        }
    }
}
