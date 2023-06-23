﻿namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒███│█████║
        // ║▒███▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒███▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒███▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒▒█▒▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒███▒│▒▒█▒█│▒▒▒▒█║
        // ╟─────┼─────┼─────╢
        // ║█▒█▒█│▒▒█▒█│▒▒▒▒█║
        // ║▒▒█▒▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒█▒█▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒█▒█▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒█▒█▒│▒▒█▒▒│▒▒▒▒█║
        // ║▒▒▒▒▒│▒▒███│█████║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
        // ╚═════╧═════╧═════╝

        public static void OpenAnimation(object CancelationObj)
        {
            CancellationToken token = (CancellationToken)CancelationObj;

            int StepTime = 120;

            byte[][] image0 = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[][] image1 = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0xE, 0xE, 0xE, 0x4, 0xE},
                                new byte[] { 0x0, 0x0, 0x7, 0x4, 0x4, 0x4, 0x4, 0x5},
                                new byte[] { 0x0, 0x0, 0x1F, 0x1, 0x1, 0x1, 0x1, 0x1},
                                new byte[] { 0x15, 0x4, 0xA, 0xA, 0xA, 0x0, 0x0, 0x0},
                                new byte[] { 0x5, 0x4, 0x4, 0x4, 0x4, 0x7, 0x0, 0x0},
                                new byte[] { 0x1, 0x1, 0x1, 0x1, 0x1, 0x1F, 0x0, 0x0} };

            byte[][] image2 = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0x7, 0x7, 0x7, 0x2, 0x7},
                                new byte[] { 0x0, 0x0, 0x7, 0x4, 0x4, 0x4, 0x4, 0x5},
                                new byte[] { 0x0, 0x0, 0x1F, 0x1, 0x1, 0x1, 0x1, 0x1},
                                new byte[] { 0xA, 0x2, 0x5, 0x5, 0x5, 0x0, 0x0, 0x0},
                                new byte[] { 0x5, 0x4, 0x4, 0x4, 0x4, 0x7, 0x0, 0x0},
                                new byte[] { 0x1, 0x1, 0x1, 0x1, 0x1, 0x1F, 0x0, 0x0} };

            byte[][] image3 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x3, 0x3, 0x3, 0x1, 0x3},
                                new byte[]{ 0x0, 0x3, 0x2, 0x2, 0x2, 0x2, 0x2, 0x3},
                                new byte[]{ 0x0, 0x10, 0xF, 0x1, 0x1, 0x1, 0x1, 0x1},
                                new byte[]{ 0x5, 0x1, 0x2, 0x2, 0x2, 0x0, 0x0, 0x0},
                                new byte[]{ 0x13, 0x2, 0x2, 0x2, 0x2, 0x2, 0x3, 0x0},
                                new byte[]{ 0x1, 0x1, 0x1, 0x1, 0x1, 0xF, 0x10, 0x0}, };

            byte[][] image4 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x1, 0x1, 0x1, 0x0, 0x1},
                                new byte[]{ 0x0, 0x3, 0x2, 0x12, 0x12, 0x12, 0x2, 0x13},
                                new byte[]{ 0x0, 0x10, 0xF, 0x1, 0x1, 0x1, 0x1, 0x1},
                                new byte[]{ 0x2, 0x0, 0x1, 0x1, 0x1, 0x0, 0x0, 0x0},
                                new byte[]{ 0xB, 0x2, 0x12, 0x12, 0x12, 0x2, 0x3, 0x0},
                                new byte[]{ 0x1, 0x1, 0x1, 0x1, 0x1, 0xF, 0x10, 0x0}, };

            byte[][] image5 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x1, 0x1, 0x1, 0x19, 0x19, 0x19, 0x11, 0x19},
                                new byte[]{ 0x10, 0xC, 0x3, 0x1, 0x1, 0x1, 0x1, 0x1},
                                new byte[]{ 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x15, 0x11, 0x9, 0x9, 0x9, 0x1, 0x1, 0x1},
                                new byte[]{ 0x1, 0x1, 0x1, 0x1, 0x1, 0x3, 0xC, 0x10}, };

            byte[][] image6 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x1C, 0x1C, 0x1C, 0x8, 0x1C},
                                new byte[]{ 0x10, 0xC, 0x3, 0x1, 0x1, 0x1, 0x1, 0x11},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0xA, 0x8, 0x14, 0x14, 0x14, 0x0, 0x0, 0x0},
                                new byte[]{ 0x11, 0x1, 0x1, 0x1, 0x1, 0x3, 0xC, 0x10}, };

            byte[][] image7 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0xE, 0xE, 0xE, 0x4, 0xE},
                                new byte[]{ 0x10, 0xC, 0x3, 0x1, 0x1, 0x1, 0x1, 0x11},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x15, 0x4, 0xA, 0xA, 0xA, 0x0, 0x0, 0x0},
                                new byte[]{ 0x11, 0x1, 0x1, 0x1, 0x1, 0x3, 0xC, 0x10}, };

            byte[][] image8 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x7, 0x7, 0x7, 0x2, 0x7},
                                new byte[]{ 0x18, 0x16, 0x11, 0x11, 0x11, 0x11, 0x11, 0x19},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0xA, 0x2, 0x5, 0x5, 0x5, 0x0, 0x0, 0x0},
                                new byte[]{ 0x19, 0x11, 0x11, 0x11, 0x11, 0x11, 0x16, 0x18}, };

            byte[][] image9 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x3, 0x3, 0x3, 0x1, 0x3},
                                new byte[]{ 0x18, 0x16, 0x11, 0x11, 0x11, 0x11, 0x11, 0x19},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x5, 0x1, 0x2, 0x2, 0x2, 0x0, 0x0, 0x0},
                                new byte[]{ 0x19, 0x11, 0x11, 0x11, 0x11, 0x11, 0x16, 0x18}, };

            byte[][] image10 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x1, 0x1, 0x1, 0x0, 0x1},
                                new byte[]{ 0x18, 0x16, 0x11, 0x11, 0x11, 0x11, 0x11, 0x19},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x2, 0x0, 0x1, 0x1, 0x1, 0x0, 0x0, 0x0},
                                new byte[]{ 0x19, 0x11, 0x11, 0x11, 0x11, 0x11, 0x16, 0x18}, };

            byte[][] image11 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x18, 0x16, 0x11, 0x11, 0x11, 0x11, 0x11, 0x19},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x19, 0x11, 0x11, 0x11, 0x11, 0x11, 0x16, 0x18}, };

            byte[][] image12 = new byte[][]
                              { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x18, 0x16, 0x11, 0x11, 0x11, 0x11, 0x11, 0x19},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[]{ 0x19, 0x11, 0x11, 0x11, 0x11, 0x11, 0x16, 0x18}, };

            byte[][][] images = new byte[][][]
                             {  image1, image2, image3, image4, image5, image5, image6, image7, image8, image9, image10, image11, image12};

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

                for (int i = 0; i < 13; i++)
                {
                    lock (send_lock)
                    {
                        for (int j = 0; j < 6; j++)
                            Screen.CreateCustomCharacter(j, images[i][j]);
                    }

                    Thread.Sleep(StepTime);
                    if (token.IsCancellationRequested)
                      break;
                }

                for (int i = 0; i < 5; ++i)
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