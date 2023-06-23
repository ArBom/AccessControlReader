using Iot.Device.CharacterLcd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AccessControlReader.LCD.Animation
{
    internal static partial class Animations
    {
        static CancellationTokenSource CancellationTokenAnimationThread;
        private static Lcd1602 Screen;
        private static ManualResetEvent finishAnim;
        private static object send_lock;

        public static void StartAnimation(Lcd1602 screen, AnimationType animationType, ref object lockScreenObj, CancellationTokenSource CancelAnimTaken, bool loop, params object[] listOfParams)
        {
            Screen = screen;
            send_lock = lockScreenObj;

            lock (lockScreenObj)
            {
                Screen.SetCursorPosition(0, 0);
                //char[] upline = new char[] { (char)0, (char)1, (char)2 };
                Screen.Write(new char[] { (char)0, (char)1, (char)2 });
                //Screen.Write(new String(new char[] { (char)0 }));
                //Screen.Write(new String(new char[] { (char)1 }));
                //Screen.Write(new String(new char[] { (char)2 }));

                Screen.SetCursorPosition(0, 1);
                Screen.Write(new char[] { (char)3, (char)4, (char)5 });
                /*Screen.Write(new String(new char[] { (char)3 }));
                Screen.Write(new String(new char[] { (char)4 }));
                Screen.Write(new String(new char[] { (char)5 }));*/
            }

            CancellationTokenAnimationThread = CancelAnimTaken;
            finishAnim = new ManualResetEvent(false);

            switch (animationType)
            {
                case AnimationType.Unactive:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(UnactiveAnimation), new object[] { loop, CancellationTokenAnimationThread.Token });
                    break;

                case AnimationType.RFID:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(RFIDAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.Open:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(OpenAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.Info:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(InfoAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.OpenAlert:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(OpenAlertAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.SecurityAlert:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(SecurityAlertAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.Hand:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(HandAnimation), CancellationTokenAnimationThread.Token);
                    break;

                case AnimationType.Error:
                    _ = ThreadPool.QueueUserWorkItem(new WaitCallback(ErrorAnimation), new object[] { true, CancellationTokenAnimationThread.Token, });
                    break;
            }
        }

        private static void CancelIconAnim()
        {
            CancellationTokenAnimationThread.Cancel();
            CancellationTokenAnimationThread = new CancellationTokenSource();
        }
    }
}
