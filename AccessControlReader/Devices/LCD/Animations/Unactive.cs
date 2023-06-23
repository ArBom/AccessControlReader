namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│█████│▒▒▒▒▒║
        // ║▒▒▒██│██▒██│██▒▒▒║
        // ║▒▒██▒│▒▒▒▒▒│▒██▒▒║
        // ║▒██▒▒│▒▒▒▒▒│▒███▒║
        // ║▒█▒▒▒│▒▒▒▒▒│████▒║
        // ║██▒▒▒│▒▒▒▒▒│██▒██║
        // ║██▒▒▒│▒▒▒▒█│█▒▒██║
        // ║██▒▒▒│▒▒▒██│▒▒▒██║
        // ╟─────┼─────┼─────╢
        // ║██▒▒▒│██▒▒▒│▒▒▒██║
        // ║██▒▒█│█▒▒▒▒│▒▒▒██║
        // ║██▒██│▒▒▒▒▒│▒▒▒██║
        // ║▒████│▒▒▒▒▒│▒▒▒█▒║
        // ║▒███▒│▒▒▒▒▒│▒▒██▒║
        // ║▒▒██▒│▒▒▒▒▒│▒██▒▒║
        // ║▒▒▒██│██▒██│██▒▒▒║
        // ║▒▒▒▒▒│█████│▒▒▒▒▒║
        // ╚═════╧═════╧═════╝

        public static void UnactiveAnimation(object objects)
        {
            if (objects is not object[] objectsArray || objectsArray.Length != 2)
            {
                throw new ArgumentException("objects must be initialized and its size must be 2.");
            }

            bool loop;
            try
            {
                loop = (bool)objectsArray[0];
            }
            catch
            {
                throw new ArgumentException("objects[0] must be the bool loop");
            }

            CancellationToken token;
            try
            {
                token = (CancellationToken)objectsArray[1];
            }
            catch
            {
                throw new ArgumentException("objects[1] must be the CancellationToken");
            }

            const int StepTime = 90;

            byte[][] fullIcon = new byte[][] 
                               { new byte[] { 0x0,  0x3,  0x6,  0xC,  0x8,  0x18, 0x18, 0x18}, 
                                 new byte[] { 0x1F, 0x1B, 0x0,  0x0,  0x0,  0x0,  0x1,  0x3 },
                                 new byte[] { 0x0,  0x18, 0xC,  0xE,  0x1E, 0x1B, 0x13, 0x3 },

                                 new byte[] { 0x18, 0x19, 0x1B, 0xF,  0xE,  0x6,  0x3,  0x0 },
                                 new byte[] { 0x18, 0x10, 0x0,  0x0,  0x0,  0x0,  0x1B, 0x1F},
                                 new byte[] { 0x3,  0x3,  0x3,  0x2,  0x6,  0xC,  0x18, 0x0 }};

            byte emptyLine = 0x00;
            byte fullLine = 0x1F;

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

                for (int i = 0; i < 5; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (token.IsCancellationRequested)
                      break;
                }

                for (int i = 0; i <= 16; ++i)
                {
                    if (i <= 8)
                    {
                        for (int j = 0; j < i; ++j)
                        {
                            TempIcons[0][j] = fullIcon[0][j];
                            TempIcons[1][j] = fullIcon[1][j];
                            TempIcons[2][j] = fullIcon[2][j];
                        }

                        if (i < 8)
                        {
                            TempIcons[0][i] = fullLine;
                            TempIcons[1][i] = fullLine;
                            TempIcons[2][i] = fullLine;
                        }

                        for (int j = i+1; j < 8; ++j)
                        {
                            TempIcons[0][j] = emptyLine;
                            TempIcons[1][j] = emptyLine;
                            TempIcons[2][j] = emptyLine;
                        }

                        lock (send_lock)
                        {
                            Screen.CreateCustomCharacter(0, TempIcons[0]);
                            Screen.CreateCustomCharacter(1, TempIcons[1]);
                            Screen.CreateCustomCharacter(2, TempIcons[2]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j+8 < i; ++j)
                        {
                            TempIcons[3][j] = fullIcon[3][j];
                            TempIcons[4][j] = fullIcon[4][j];
                            TempIcons[5][j] = fullIcon[5][j];
                        }

                        if (i < 16)
                        {
                            TempIcons[3][i - 8] = fullLine;
                            TempIcons[4][i - 8] = fullLine;
                            TempIcons[5][i - 8] = fullLine;
                        }

                        lock (send_lock)
                        {
                            Screen.CreateCustomCharacter(3, TempIcons[3]);
                            Screen.CreateCustomCharacter(4, TempIcons[4]);
                            Screen.CreateCustomCharacter(5, TempIcons[5]);
                        }
                    }

                    if (token.IsCancellationRequested)
                        break;

                    finishAnim.Set();

                    Thread.Sleep(StepTime);
                }

                for (int i = 0; i < 15; ++i)
                {
                    Thread.Sleep(StepTime);

                    if (!loop)
                        i = 0;

                    if (token.IsCancellationRequested)
                        break;
                }      
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