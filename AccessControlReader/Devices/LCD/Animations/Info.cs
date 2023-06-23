namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│█████│▒▒▒▒▒║
        // ║▒▒▒▒█│▒▒▒▒▒│█▒▒▒▒║
        // ║▒▒██▒│▒▒▒▒▒│▒██▒▒║
        // ║▒▒█▒▒│▒▒▒██│▒▒█▒▒║
        // ║▒█▒▒▒│▒▒▒██│▒▒▒█▒║
        // ║▒█▒▒▒│▒▒▒▒▒│▒▒▒█▒║
        // ║█▒▒▒▒│████▒│▒▒▒▒█║
        // ║█▒▒▒▒│█▒██▒│▒▒▒▒█║
        // ╟─────┼─────┼─────╢
        // ║█▒▒▒▒│▒▒██▒│▒▒▒▒█║
        // ║█▒▒▒▒│▒███▒│▒▒▒▒█║
        // ║▒█▒▒▒│▒██▒▒│▒▒▒█▒║
        // ║▒█▒▒▒│▒██▒█│▒▒▒█▒║
        // ║▒▒█▒▒│▒███▒│▒▒█▒▒║
        // ║▒▒██▒│▒▒▒▒▒│▒██▒▒║
        // ║▒▒▒▒█│▒▒▒▒▒│█▒▒▒▒║
        // ║▒▒▒▒▒│█████│▒▒▒▒▒║
        // ╚═════╧═════╧═════╝

        public static void InfoAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;

            byte[][] empty = new byte[][]
                  { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[][] circle0 = new byte[][]
                  { new byte[] {0x0, 0x1, 0x6, 0x4, 0x8, 0x8, 0x10, 0x10},
                    new byte[] {0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] {0x0, 0x10, 0xC, 0x4, 0x2, 0x2, 0x1, 0x1},
                    new byte[] {0x10, 0x10, 0x8, 0x8, 0x4, 0x6, 0x1, 0x0},
                    new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F},
                    new byte[] {0x1, 0x1, 0x2, 0x2, 0x4, 0xC, 0x10, 0x0}, };

            byte[] sign1image1 = new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10 };
            byte[] sign1image2 = new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x10 };
            byte[] sign1image3 = new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x10 };
            byte[] sign1image4 = new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1C, 0x10 };
            byte[] sign1image5 = new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1E, 0x16 };

            byte[] sign4image6 = new byte[] { 0x2, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F };
            byte[] sign4image7 = new byte[] { 0x6, 0x2, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F };
            byte[] sign4image8 = new byte[] { 0x6, 0x6, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F };
            byte[] sign4image9 = new byte[] { 0x6, 0xE, 0x4, 0x0, 0x0, 0x0, 0x0, 0x1F };
            
            byte[] sign4image10 = new byte[] { 0x6, 0xE, 0xC, 0x0, 0x0, 0x0, 0x0, 0x1F };
            byte[] sign4image11 = new byte[] { 0x6, 0xE, 0xC, 0x8, 0x0, 0x0, 0x0, 0x1F };
            byte[] sign4image12 = new byte[] { 0x6, 0xE, 0xC, 0xC, 0x8, 0x0, 0x0, 0x1F };
            byte[] sign4image13 = new byte[] { 0x6, 0xE, 0xC, 0xC, 0xC, 0x0, 0x0, 0x1F };
            byte[] sign4image14 = new byte[] { 0x6, 0xE, 0xC, 0xC, 0xE, 0x0, 0x0, 0x1F };
            byte[] sign4image15 = new byte[] { 0x6, 0xE, 0xC, 0xD, 0xE, 0x0, 0x0, 0x1F };

            byte[] sign1image16 = new byte[] { 0x1F, 0x0, 0x0, 0x3, 0x3, 0x0, 0x1E, 0x16 };


            byte[][] infoIcon = new byte[][]
                  { new byte[] {0x0, 0x1, 0x6, 0x4, 0x8, 0x8, 0x10, 0x10},
                    new byte[] {0x1F, 0x0, 0x3, 0x3, 0x0, 0x1E, 0x16, 0x6},
                    new byte[] {0x0, 0x10, 0xC, 0x4, 0x2, 0x2, 0x1, 0x1},
                    new byte[] {0x10, 0x10, 0x8, 0x8, 0x4, 0x6, 0x1, 0x0},
                    new byte[] {0x6, 0xE, 0xC, 0xC, 0xD, 0xE, 0x0, 0x1F},
                    new byte[] {0x1, 0x1, 0x2, 0x2, 0x4, 0xC, 0x10, 0x0}, };

            int StepTime = 100;

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, circle0[i]);
            }

            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < 8; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                        break;
                }

                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, circle0[i]);
                }

                for (int i = 0; i < 3; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                        break;
                }

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image1);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image2);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image3);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image4);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image5);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image6);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image7);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image8);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image9);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image10);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image11);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image12);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image13);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image14);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(4, sign4image15);
                Thread.Sleep(3*StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image16);
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;
            }

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, empty[i]);
            }

        }
    }
}