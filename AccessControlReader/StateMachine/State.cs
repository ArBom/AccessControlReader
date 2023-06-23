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

        protected List<Task> StateTasks;

        protected readonly string details;

        protected CancellationTokenSource _currentState_CancelationTokenSource;

        protected Dictionary<EventType, Type> reactTo;
        public State(string details)
        {
            StateTasks = new List<Task>();
            _currentState_CancelationTokenSource = new CancellationTokenSource();
            reactTo = new Dictionary<EventType, Type>();
            AddEvents();

            this.details = details;
        }

        public static void SetTexts(XElement Config)
        {
            if (Config is null)
            {
                //TODO obsługa w przypadku null
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
            catch
            {

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
            _currentState_CancelationTokenSource?.Cancel();
            OnExit();
            Task.WhenAll(StateTasks);
        }
    }
}
