using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Reflection;

namespace AccessControlReader.StateMachine.States
{
    internal class InitState : State
    {
        public string macAddr = "not found";
        public string ipAddr = "not connected";

        public InitState(string details) : base(details) 
        { 
            this.OnEnter();
        }

        protected override void AddEvents() 
        {
            reactTo.Add(EventType.MainState_Unconfig, typeof(UnconfigState));
            reactTo.Add(EventType.MainState_Reading, typeof(ReadingState));
            reactTo.Add(EventType.MainState_Blocked, typeof(BlockedState));
        }

        public override void OnEnter() 
        {
            Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
            var VersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location);
            string version = VersionInfo.ProductVersion;

            Console.WriteLine("Version............: " + version);
            Console.WriteLine("MAC addr...........: " + macAddr);
            Console.WriteLine("IP addr............: " + ipAddr);

            Task infoShowing = new(() => 
            {
                while (_screen is null)
                    Task.Delay(250);

                _screen.Icon = false;

                while (!_currentState_CancelationTokenSource.IsCancellationRequested)
                {
                    _screen?.Write(@"Version:\n" + version);
                    Task.Delay(750).Wait();
                    if (_currentState_CancelationTokenSource.IsCancellationRequested)
                        break;

                    _screen?.Write(@"MAC address:\n" + macAddr);
                    Task.Delay(2000).Wait();
                    if (_currentState_CancelationTokenSource.IsCancellationRequested)
                        break;

                    _screen?.Write(@"IP address:\n" + ipAddr);
                    Task.Delay(1000).Wait();
                    if (_currentState_CancelationTokenSource.IsCancellationRequested)
                        break;

                    if (_gpio != null)
                    {
                        _screen?.Write(@"GPIO module:\nOK");
                        Task.Delay(500).Wait();
                        if (_currentState_CancelationTokenSource.IsCancellationRequested)
                            break;
                    }

                    if (_rC522 != null)
                    {
                        _screen?.Write(@"Reader module:\nOK");
                        Task.Delay(500).Wait();
                        if (_currentState_CancelationTokenSource.IsCancellationRequested)
                            break;
                    }
                }
            });

            infoShowing.Start();
            StateTasks.Add(infoShowing);
        }

        protected override void OnExit() 
        {
            if (_screen is not null)
                _screen.Icon = true;
        }
    }
}