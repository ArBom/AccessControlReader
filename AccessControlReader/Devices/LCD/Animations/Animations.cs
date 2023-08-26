using Iot.Device.CharacterLcd;

namespace AccessControlReader.LCD.Animation
{
    abstract class Animation
    {
        readonly private int DefaultStepTime;

        protected int CurrentStepTime { private set; get; }

        protected Animation(int DefaultStepTime)
        {
            this.DefaultStepTime = DefaultStepTime;
            CurrentStepTime = DefaultStepTime;
        }
        public abstract void StartAnimations(Lcd1602 Screen, object send_lock, bool loop, CancellationTokenSource token);

        public int PauseAnimation(int PauseTime)
        {
            CurrentStepTime = PauseTime;
            Task.Factory.StartNew(() =>
            {
                Task.Delay(PauseTime - DefaultStepTime);
                CurrentStepTime = DefaultStepTime;
            });

            return DefaultStepTime;
        }
    }

    class Animations
    {
        private Task CurrentAnimation = null;
        private Animation currentAnimClass;

        readonly private Lcd1602 screen;
        readonly private object send_lock;

        public Animations(Lcd1602 screen, object send_lock) 
        {
            this.screen = screen;
            this.send_lock = send_lock;
        }

        public void ShowErrorIcons(ErrorImportant errorImportant,
                                   ErrorTypeIcon[] listOfParams,
                                   CancellationTokenSource? cancellationTokenSource)
        {
            int TimeOfErrorAnimation = 0;

            switch (errorImportant)
            {
                case ErrorImportant.Info:
                    //Show info error for 10s
                    TimeOfErrorAnimation = 10_000; 
                    break;

                case ErrorImportant.Warning:
                    //Show warning error for 60s
                    TimeOfErrorAnimation = 60_000;
                    break;

                case ErrorImportant.Critical:
                    //Show critical error for 1h
                    TimeOfErrorAnimation = 36_00_000;
                    break;
            }

            int TimeToWait = 0;
            if (currentAnimClass is not null)
            {
                TimeToWait = currentAnimClass.PauseAnimation(TimeOfErrorAnimation);
            }

            var a = new ErrorAnimation(listOfParams);

            CancellationTokenSource cancellationErrorAnim = cancellationTokenSource is not null ? cancellationTokenSource : new(); 

            //wait for one frame of current animation
            Task.Delay(TimeToWait);

            //show error anim...
            Task.Run(() => a.StartAnimations(screen, send_lock, true, cancellationErrorAnim));

            //...and cancel it after time
            try 
            { cancellationErrorAnim.CancelAfter(TimeOfErrorAnimation); }
            //There is no sens to catch it
            catch { }
            
        }

        public void StartAnimation(AnimationType animationType,
                                   CancellationTokenSource CancelAnimTaken,
                                   bool loop,
                                   params ErrorTypeIcon[] listOfParams)
        {
            switch (animationType)
            {
                case AnimationType.Unactive:
                    currentAnimClass = new UnactiveAnimation();
                    break;

                case AnimationType.RFID:
                    currentAnimClass = new RFIDAnimation();
                    break;

                case AnimationType.Open:
                    currentAnimClass = new OpenAnimation();
                    break;

                case AnimationType.Info:
                    currentAnimClass = new InfoAnimation();
                    break;

                case AnimationType.OpenAlert:
                    currentAnimClass = new OpenAlertAnimation();
                    break;

                case AnimationType.SecurityAlert:
                    currentAnimClass = new SecurityAlertAnimation();
                    break;

                case AnimationType.Hand:
                    currentAnimClass = new HandAnimation();
                    break;

                case AnimationType.Error:
                    currentAnimClass = new ErrorAnimation(listOfParams);
                    break;
            }

            this.CurrentAnimation = Task.Run(() => currentAnimClass.StartAnimations(screen, send_lock, loop, CancelAnimTaken));
        }
    }
}
