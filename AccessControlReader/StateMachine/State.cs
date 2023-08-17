using AccessControlReader.Devices;
using AccessControlReader.StateMachine.States;
using System.Xml.Linq;

namespace AccessControlReader.StateMachine
{
    abstract class State : IDisposable
    {
        public static Noises _noises;
        public static GPIO _gpio;
        public static RC522 _rC522;
        public static Screen _screen;

        //Activities of state, like: GPIO output, animation, etc.
        protected List<Task> StateTasks;

        protected readonly string details;

        //Token make of to cancel StateTasks
        protected CancellationTokenSource _currentState_CancelationTokenSource;

        //Possible transition for this state
        protected Dictionary<EventType, Type> reactTo;

        public State(string details)
        {
            StateTasks = new List<Task>();
            _currentState_CancelationTokenSource = new CancellationTokenSource();
            reactTo = new Dictionary<EventType, Type>();
            AddEvents();

            this.details = details;
        }

        public static ErrorEvent errorEvent;

        public static void SetTexts(XElement Config)
        {
            if (Config is null)
            {
                errorEvent(typeof(State), "State: Config is null", 60, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML });
            }

            try
            {
                ReadingState.DefaultMessage = Config.Element("Ready").Value;
                ReOpenRFIDState.DefaultMessage = Config.Element("Open").Value;
                ReWrongCardState.DefaultMessage = Config.Element("NoGo").Value;
                ReToLongTimeOpenState.DefaultMessage = Config.Element("CloseDoorAlert").Value;
                BlockedState.DefaultMessage = Config.Element("UnActive").Value;
                ReUnaOpenState.DefaultMessage = Config.Element("UnaOpen").Value;
                BlUnaOpenState.DefaultMessage = Config.Element("UnaOpen").Value;
                ReStillOpenState.DefaultMessage = Config.Element("DoorReminder").Value;
                ReOpenButtonState.DefaultMessage = Config.Element("ButtonClick").Value;
            }
            catch( Exception ex)
            {
                errorEvent(typeof(State), ex.ToString(), 61, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML });
            }
        }

        public abstract void OnEnter();
        protected abstract void AddEvents();
        protected abstract void OnExit();

        public State EnterNewState(EventType eventType, string details)
        {
            Type NewStateType = reactTo.GetValueOrDefault(eventType, null);

            if (NewStateType is null)
                return null;

            object[] args = { details };
            this.Dispose();
            State newState = (State)Activator.CreateInstance(NewStateType, args);
            return newState;
        }

        public void Dispose()
        {
            _currentState_CancelationTokenSource.Cancel();
            OnExit();
            Task.WhenAll(StateTasks);
        }
    }
}