namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│▒▒▒██│▒▒▒▒▒║
        // ║▒▒▒▒▒│██▒██│██▒▒▒║
        // ║▒▒▒▒▒│██▒██│██▒▒▒║
        // ║▒▒▒▒▒│██▒██│██▒██║
        // ║▒▒▒▒▒│██▒██│██▒██║
        // ║▒▒▒▒▒│██▒██│██▒██║
        // ║▒▒▒▒▒│██▒██│██▒██║
        // ║▒▒▒▒▒│██▒██│██▒██║
        // ╟─────┼─────┼─────╢
        // ║▒▒▒▒▒│█████│█████║
        // ║███▒▒│█████│█████║
        // ║████▒│█████│█████║
        // ║▒████│█████│█████║
        // ║▒▒███│█████│█████║
        // ║▒▒▒██│█████│████▒║
        // ║▒▒▒▒█│█████│████▒║
        // ║▒▒▒▒▒│█████│███▒▒║
        // ╚═════╧═════╧═════╝

        public static void HandAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;
            int StepTime = 90;

            byte[][] image0 = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[] sign4image1 = new byte[] { 0x0, 0x0, 0x0, 0x1, 0x1, 0x0, 0x0, 0x0 };
            byte[] sign5image1 = new byte[] { 0x0, 0x0, 0x0, 0x10, 0x10, 0x0, 0x0, 0x0 };

            byte[] sign4image2 = new byte[] { 0x0, 0x0, 0x3, 0x3, 0x3, 0x3, 0x0, 0x0 };
            byte[] sign5image2 = new byte[] { 0x0, 0x0, 0x18, 0x18, 0x18, 0x18, 0x0, 0x0 };

            byte[] sign4image3 = new byte[] { 0x0, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x0 };
            byte[] sign5image3 = new byte[] { 0x0, 0x1C, 0x1C, 0x1C, 0x1C, 0x1C, 0x1C, 0x0 };

            byte[] sign4image4 = new byte[] { 0x0, 0xF, 0xF, 0xF, 0xF, 0xF, 0xF, 0x0 };
            byte[] sign5image4 = new byte[] { 0x0, 0x1E, 0x1E, 0x1E, 0x1E, 0x1E, 0x1C, 0x0 };

            byte[] sign4image5 = new byte[] { 0x1F, 0x1F, 0x1F, 0x1F, 0x1F, 0x1F, 0x1F, 0xF };
            byte[] sign5image5 = new byte[] { 0x1F, 0x1F, 0x1F, 0x1F, 0x1F, 0x1E, 0x1E, 0x1C };

            byte[] sign1image6 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x3 };

            byte[] sign1image7 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x3, 0x1B };
            byte[] sign2image7 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1B };
            byte[] sign3image7 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x1, 0x1, 0x0, 0x0 };

            byte[] sign1image8 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x3, 0x1B, 0x1B };
            byte[] sign2image8 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1B, 0x1B };
            byte[] sign3image8 = new byte[] { 0x0, 0x0, 0x0, 0x3, 0x3, 0x3, 0x1, 0x0 };

            byte[] sign1image9 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x3, 0x1B, 0x1B, 0x1B };
            byte[] sign2image9 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x1B, 0x1B };

            byte[] sign1image10 = new byte[] { 0x0, 0x0, 0x0, 0x3, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign2image10 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x18, 0x1B, 0x1B, 0x1B };
            byte[] sign3image10 = new byte[] { 0x0, 0x0, 0x6, 0x7, 0x7, 0x3, 0x1, 0x0 };

            byte[] sign1image11 = new byte[] { 0x0, 0x0, 0x3, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign2image11 = new byte[] { 0x0, 0x0, 0x0, 0x18, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign3image11 = new byte[] { 0x0, 0x0, 0xC, 0xE, 0xF, 0x3, 0x1, 0x0 };

            byte[] sign1image12 = new byte[] { 0x0, 0x3, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign2image12 = new byte[] { 0x0, 0x0, 0x18, 0x18, 0x1B, 0x1B, 0x1B, 0x1B };

            byte[] sign1image13 = new byte[] { 0x0, 0x3, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign2image13 = new byte[] { 0x0, 0x0, 0x18, 0x18, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign3image13 = new byte[] { 0x0, 0x0, 0xC, 0xE, 0xF, 0x3, 0x1, 0x0 };

            byte[] sign1image14 = new byte[] { 0x3, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign2image14 = new byte[] { 0x0, 0x18, 0x18, 0x1B, 0x1B, 0x1B, 0x1B, 0x1B };
            byte[] sign3image14 = new byte[] { 0x0, 0x1C, 0x1E, 0xF, 0x7, 0x3, 0x1, 0x0 };

            while (!token.IsCancellationRequested)
            {
                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, image0[i]);
                }

                for (int i = 0; i < 3; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                      break;
                }

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(4, sign4image1);
                    Screen.CreateCustomCharacter(5, sign5image1);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(4, sign4image2);
                    Screen.CreateCustomCharacter(5, sign5image2);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(4, sign4image3);
                    Screen.CreateCustomCharacter(5, sign5image3);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(4, sign4image4);
                    Screen.CreateCustomCharacter(5, sign5image4);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(4, sign4image5);
                    Screen.CreateCustomCharacter(5, sign5image5);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image6);

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image7);
                    Screen.CreateCustomCharacter(2, sign2image7);
                    Screen.CreateCustomCharacter(3, sign3image7);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image8);
                    Screen.CreateCustomCharacter(2, sign2image8);
                    Screen.CreateCustomCharacter(3, sign3image8);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image9);
                    Screen.CreateCustomCharacter(2, sign2image9);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image10);
                    Screen.CreateCustomCharacter(2, sign2image10);
                    Screen.CreateCustomCharacter(3, sign3image10);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image11);
                    Screen.CreateCustomCharacter(2, sign2image11);
                    Screen.CreateCustomCharacter(3, sign3image11);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image12);
                    Screen.CreateCustomCharacter(2, sign2image12);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image13);
                    Screen.CreateCustomCharacter(2, sign2image13);
                    Screen.CreateCustomCharacter(3, sign3image13);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(1, sign1image14);
                    Screen.CreateCustomCharacter(2, sign2image14);
                    Screen.CreateCustomCharacter(3, sign3image14);
                }
                for (int i = 0; i < 21; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                      break;
                }

                finishAnim.Set();
            }

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, image0[i]);
            }
        }
    }
}