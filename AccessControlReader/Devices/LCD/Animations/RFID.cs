namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒█│██▒▒▒│▒▒▒▒▒║
        // ║▒▒███│▒███▒│▒▒▒▒▒║
        // ║▒██▒▒│▒▒▒██│▒▒▒▒▒║
        // ║▒█▒▒█│██▒▒█│▒▒▒▒▒║
        // ║██▒█▒│▒▒█▒█│▒▒▒▒▒║
        // ║█▒▒█▒│█▒█▒█│▒▒▒▒▒║
        // ║█▒▒█▒│█▒█▒█│▒▒▒▒▒║
        // ╟─────┼─────┼─────╢
        // ║██▒█▒│▒████│█████║
        // ║▒█▒▒█│██▒▒▒│▒▒▒▒█║
        // ║▒▒█▒▒│█▒▒▒▒│▒▒▒▒█║
        // ║▒▒▒██│█▒▒██│▒▒▒▒█║
        // ║▒▒▒▒▒│█▒▒██│▒▒▒▒█║
        // ║▒▒▒▒▒│█▒▒▒▒│▒▒▒▒█║
        // ║▒▒▒▒▒│█▒▒▒▒│▒▒▒▒█║
        // ║▒▒▒▒▒│█████│█████║
        // ╚═════╧═════╧═════╝


        public static void RFIDAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;
            const int StepTime = 150;

            byte[] sign4image1 = new byte[] { 0xF, 0x18, 0x10, 0x13, 0x13, 0x10, 0x10, 0x1F };
            byte[] sign5image1 = new byte[] { 0x1F, 0x1, 0x1, 0x1, 0x1, 0x1, 0x1, 0x1F };

            byte[] sign1image2 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x10 };

            byte[] sign0image3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x1, 0x1 };
            byte[] sign1image3 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x8, 0x8 };
            byte[] sign3image3 = new byte[] { 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image4 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x1, 0x2, 0x2, 0x2 };
            byte[] sign1image4 = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x18, 0x4, 0x14, 0x14 };
            byte[] sign3image4 = new byte[] { 0x2, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image5 = new byte[] { 0x0, 0x0, 0x0, 0x3, 0x6, 0x4, 0xD, 0x9 };
            byte[] sign1image5 = new byte[] { 0x0, 0x0, 0x10, 0x1C, 0x6, 0x1A, 0xA, 0xA };
            byte[] sign3image5 = new byte[] { 0x5, 0x6, 0x3, 0x0, 0x0, 0x0, 0x0, 0x0 };

            byte[] sign0image6 = new byte[] { 0x0, 0x1, 0x7, 0xC, 0x9, 0x1A, 0x12, 0x12};
            byte[] sign1image6 = new byte[] { 0x0, 0x18, 0xE, 0x3, 0x19, 0x5, 0x15, 0x15};
            byte[] sign3image6 = new byte[] { 0x1A, 0x9, 0x4, 0x3, 0x0, 0x0, 0x0, 0x0 };

            while (!token.IsCancellationRequested)
            {
                // ╔═╤═╤═╗
                // ║0│1│2║
                // ╟─┼─┼─╢
                // ║3│4│5║
                // ╚═╧═╧═╝
                byte[][] TempIcons = new byte[][]
                                     { new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                                       new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                                       new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },

                                       new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                                       new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                                       new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }};

                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, TempIcons[i]);
                }

                for (int i = 0; i < 4; ++i)
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

                for (int i = 0; i < 4; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                      break;
                }

                lock (send_lock)
                    Screen.CreateCustomCharacter(1, sign1image2);

                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image3);
                    Screen.CreateCustomCharacter(1, sign1image3);
                    Screen.CreateCustomCharacter(3, sign3image3);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image4);
                    Screen.CreateCustomCharacter(1, sign1image4);
                    Screen.CreateCustomCharacter(3, sign3image4);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image5);
                    Screen.CreateCustomCharacter(1, sign1image5);
                    Screen.CreateCustomCharacter(3, sign3image5);
                }
                Thread.Sleep(StepTime);
                if (token.IsCancellationRequested)
                  break;

                lock (send_lock)
                {
                    Screen.CreateCustomCharacter(0, sign0image6);
                    Screen.CreateCustomCharacter(1, sign1image6);
                    Screen.CreateCustomCharacter(3, sign3image6);
                }
                for (int i = 0; i < 12; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                      break;
                }

                finishAnim.Set();
            }

            byte[] EmptySign = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, EmptySign);
            }
        }
    }
}