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

    internal static partial class Animations
    {
        private static Task CurrentAnimation = null;

        public static void StartAnimation(Lcd1602 screen, AnimationType animationType, object send_lock, CancellationTokenSource CancelAnimTaken, bool loop, params ErrorTypeIcon[] listOfParams)
        {
            Animation currentAnimClass = null;

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

            CurrentAnimation?.Dispose();
            CurrentAnimation = Task.Run(() => currentAnimClass.StartAnimations(screen, send_lock, loop, CancelAnimTaken));
        }
    }
}
