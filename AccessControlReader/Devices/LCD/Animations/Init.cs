using System.Reflection.PortableExecutable;

/*
namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║██▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║██▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ╟─────┼─────┼─────╢
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ╚═════╧═════╧═════╝

        public static void InitAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;

            byte[][] empty = new byte[][]
                  { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[] signEmpty = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] signUp0n = new byte[] { 0x10, 0x10, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signUp1 = new byte[] { 0x18, 0x18, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signUp2 = new byte[] { 0xC, 0xC, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signUp3 = new byte[] { 0x6, 0x6, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signUp4 = new byte[] { 0x3, 0x3, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signUp5n = new byte[] { 0x1, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] signRi0n = new byte[] { 0x3, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signRi2 = new byte[] { 0x0, 0x3, 0x3, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signRi3 = new byte[] { 0x0, 0x0, 0x3, 0x3, 0x0, 0x0, 0x0, 0x0 };
            byte[] signRi4 = new byte[] { 0x0, 0x0, 0x0, 0x3, 0x3, 0x0, 0x0, 0x0 };
            byte[] signRi5 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x3, 0x3, 0x0, 0x0 };
            byte[] signRi6 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x3, 0x3, 0x0 };
            byte[] signRi8n = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x3 };

            byte[] signDn0n = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x1 };
            byte[] signDn1 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x3, 0x3 };
            byte[] signDn2 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x6, 0x6 };
            byte[] signDn3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC, 0xC };
            byte[] signDn4 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x18 };
            byte[] signDn5n = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x10 };

            byte[] signLe0n = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x18 };
            byte[] signLe2 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x18, 0x0 };
            byte[] signLe3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x18, 0x18, 0x0, 0x0 };
            byte[] signLe4 = new byte[] { 0x0, 0x0, 0x0, 0x18, 0x18, 0x0, 0x0, 0x0 };
            byte[] signLe5 = new byte[] { 0x0, 0x0, 0x18, 0x18, 0x0, 0x0, 0x0, 0x0 };
            byte[] signLe6 = new byte[] { 0x0, 0x18, 0x18, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] signLe8n = new byte[] { 0x18, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            int StepTime = 50;

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, empty[i]);
            }

            while (!token.IsCancellationRequested)
            {
                //sign 0 up
                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signUp1);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signUp2);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signUp3);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signUp4);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signUp5n);
                }
                Thread.Sleep(StepTime);

                //sign 1 up
                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, signEmpty);
                    Screen.CreateCustomCharacter(1, signUp0n);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, signUp1);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, signUp2);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, signUp3);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, signUp4);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, signUp5n);
                }
                Thread.Sleep(StepTime);

                //sign 2 up
                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signUp0n);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signUp1);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signUp2);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signUp3);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signUp4);
                }
                Thread.Sleep(StepTime);

                //sign 2 right
                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi2);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi3);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi4);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi5);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi6);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signDn1);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signRi8n);
                }
                Thread.Sleep(StepTime);

                //sign 6 right
                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(2, signEmpty);
                    Screen.CreateCustomCharacter(6, signRi0n);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signUp4);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signRi2);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signRi3);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signRi4);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signRi5);
                }
                Thread.Sleep(StepTime);

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(6, signRi6);
                }
                Thread.Sleep(StepTime);

            }

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, empty[i]);
            }

        }
    }
}*/