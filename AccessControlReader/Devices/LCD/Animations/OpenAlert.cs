namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒█▒▒│█████│▒▒█▒▒║
        // ║▒█▒▒▒│█▒▒▒█│▒▒▒█▒║
        // ║█▒▒▒▒│█▒▒▒█│▒▒▒▒█║
        // ║█▒▒█▒│█▒▒▒█│▒█▒▒█║
        // ║█▒█▒▒│█▒▒▒█│▒▒█▒█║
        // ║█▒█▒█│██▒▒█│█▒█▒█║
        // ╟─────┼─────┼─────╢
        // ║█▒█▒█│██▒▒█│█▒█▒█║
        // ║█▒█▒▒│█▒▒▒█│▒▒█▒█║
        // ║█▒▒█▒│█▒▒▒█│▒█▒▒█║
        // ║█▒▒▒▒│█▒▒▒█│▒▒▒▒█║
        // ║▒█▒▒▒│█▒▒▒█│▒▒▒█▒║
        // ║▒▒█▒▒│█████│▒▒█▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ╚═════╧═════╧═════╝

        public static void OpenAlertAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;

            const int StepTime = 110;

            byte[][] image0 = new byte[][]
                  { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                    new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[] sign1image1 = new byte[] { 0x0, 0x0, 0x1F, 0x11, 0x11, 0x11, 0x11, 0x19};
            byte[] sign4image1 = new byte[] { 0x19, 0x11, 0x11, 0x11, 0x11, 0x1F, 0x0, 0x0};

            byte[] sign0image2 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1 };
            byte[] sign2image2 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10 };
            byte[] sign3image2 = new byte[] { 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image2 = new byte[] { 0x10, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x2 };
            byte[] sign2image3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x8 };
            byte[] sign3image3 = new byte[] { 0x2, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image3 = new byte[] { 0x8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            
            byte[] sign0image4 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x2, 0x4, 0x5 };
            byte[] sign2image4 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x8, 0x4, 0x15 };
            byte[] sign3image4 = new byte[] { 0x5, 0x4, 0x2, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image4 = new byte[] { 0x14, 0x4, 0x8, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image5 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x8, 0xA, 0xA };
            byte[] sign2image5 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x2, 0xA, 0xA };
            byte[] sign3image5 = new byte[] { 0xA, 0xA, 0x8, 0x4, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image5 = new byte[] { 0xA, 0xA, 0x2, 0x4, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image6 = new byte[] { 0x0, 0x0, 0x4, 0x8, 0x10, 0x12, 0x14, 0x15 };
            byte[] sign2image6 = new byte[] { 0x0, 0x0, 0x4, 0x2, 0x1, 0x9, 0x5, 0x15 };
            byte[] sign3image6 = new byte[] { 0x15, 0x14, 0x12, 0x10, 0x8, 0x4, 0x0, 0x0 };	
            byte[] sign5image6 = new byte[] { 0x15, 0x5, 0x9, 0x1, 0x2, 0x4, 0x0, 0x0 };

            byte[] sign0image7 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x8, 0xA, 0xA };
            byte[] sign2image7 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x2, 0xA, 0xA };
            byte[] sign3image7 = new byte[] { 0xA, 0xA, 0x8, 0x4, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image7 = new byte[] { 0xA, 0xA, 0x2, 0x4, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image8 = new byte[] { 0x0, 0x0, 0x4, 0x8, 0x10, 0x12, 0x14, 0x14 };
            byte[] sign2image8 = new byte[] { 0x0, 0x0, 0x4, 0x2, 0x1, 0x9, 0x5, 0x5 };
            byte[] sign3image8 = new byte[] { 0x14, 0x14, 0x12, 0x10, 0x8, 0x4, 0x0, 0x0 };
            byte[] sign5image8 = new byte[] { 0x5, 0x5, 0x9, 0x1, 0x2, 0x4, 0x0, 0x0 };

            byte[] sign0image9 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x8, 0x8, 0x8 };
            byte[] sign2image9 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x4, 0x2, 0x2, 0x2 };
            byte[] sign3image9 = new byte[] { 0x8, 0x8, 0x8, 0x4, 0x0, 0x0, 0x0, 0x0 };
            byte[] sign5image9 = new byte[] { 0x2, 0x2, 0x2, 0x4, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image10 = new byte[] { 0x0, 0x0, 0x4, 0x8, 0x10, 0x10, 0x10, 0x10 };
            byte[] sign2image10 = new byte[] { 0x0, 0x0, 0x4, 0x2, 0x1, 0x1, 0x1, 0x1 };
            byte[] sign3image10 = new byte[] { 0x10, 0x10, 0x10, 0x10, 0x8, 0x4, 0x0, 0x0 };
            byte[] sign5image10 = new byte[] { 0x1, 0x1, 0x1, 0x1, 0x2, 0x4, 0x0, 0x0 };

            // ╔═╤═╤═╗
            // ║0│1│2║
            // ╟─┼─┼─╢
            // ║3│4│5║
            // ╚═╧═╧═╝

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, image0[i]);
            }

            Thread.Sleep(StepTime);

            lock (send_lock)
            {
                Screen.CreateCustomCharacter(1, sign1image1);
                Screen.CreateCustomCharacter(4, sign4image1);
            }

            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image2);
                    Screen.CreateCustomCharacter(2, sign2image2);
                    Screen.CreateCustomCharacter(3, sign3image2);
                    Screen.CreateCustomCharacter(5, sign5image2);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image3);
                    Screen.CreateCustomCharacter(2, sign2image3);
                    Screen.CreateCustomCharacter(3, sign3image3);
                    Screen.CreateCustomCharacter(5, sign5image3);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image4);
                    Screen.CreateCustomCharacter(2, sign2image4);
                    Screen.CreateCustomCharacter(3, sign3image4);
                    Screen.CreateCustomCharacter(5, sign5image4);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image5);
                    Screen.CreateCustomCharacter(2, sign2image5);
                    Screen.CreateCustomCharacter(3, sign3image5);
                    Screen.CreateCustomCharacter(5, sign5image5);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image6);
                    Screen.CreateCustomCharacter(2, sign2image6);
                    Screen.CreateCustomCharacter(3, sign3image6);
                    Screen.CreateCustomCharacter(5, sign5image6);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image7);
                    Screen.CreateCustomCharacter(2, sign2image7);
                    Screen.CreateCustomCharacter(3, sign3image7);
                    Screen.CreateCustomCharacter(5, sign5image7);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image8);
                    Screen.CreateCustomCharacter(2, sign2image8);
                    Screen.CreateCustomCharacter(3, sign3image8);
                    Screen.CreateCustomCharacter(5, sign5image8);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image9);
                    Screen.CreateCustomCharacter(2, sign2image9);
                    Screen.CreateCustomCharacter(3, sign3image9);
                    Screen.CreateCustomCharacter(5, sign5image9);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image10);
                    Screen.CreateCustomCharacter(2, sign2image10);
                    Screen.CreateCustomCharacter(3, sign3image10);
                    Screen.CreateCustomCharacter(5, sign5image10);
                }

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, image0[0]);
                    Screen.CreateCustomCharacter(2, image0[2]);
                    Screen.CreateCustomCharacter(3, image0[3]);
                    Screen.CreateCustomCharacter(5, image0[5]);
                }

                for (int i = 0; i < 3; ++i)
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