using Iot.Device.CharacterLcd;

namespace AccessControlReader.LCD.Animation
{
    class SecurityAlertAnimation : Animation
    {
        // ╔═════╤═════╤═════╗
        // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│▒███▒│▒▒▒▒▒║
        // ║▒▒▒▒▒│█████│▒▒▒▒▒║
        // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
        // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
        // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
        // ║▒▒▒▒█│█▒▒▒█│█▒▒▒▒║
        // ║▒▒▒▒█│█▒▒▒█│█▒▒▒▒║
        // ╟─────┼─────┼─────╢
        // ║▒▒▒██│█▒▒▒█│██▒▒▒║
        // ║▒▒▒██│█▒▒▒█│██▒▒▒║
        // ║▒▒███│█▒▒▒█│███▒▒║
        // ║▒▒███│█████│███▒▒║
        // ║▒████│█▒▒▒█│████▒║
        // ║▒████│█▒▒▒█│████▒║
        // ║█████│█▒▒▒█│█████║
        // ║█████│█████│█████║
        // ╚═════╧═════╧═════╝

        public SecurityAlertAnimation() : base(750) { }

        public override void StartAnimations(Lcd1602 Screen, object send_lock, bool loop, CancellationTokenSource token)
        {
            #region DeclareAnimationData
            //const int CurrentStepTime = 750;

            byte[][] image0 = new byte[][]
                  { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            byte[][] image1 = new byte[][]
                                 { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x1},
                                   new byte[] { 0x4, 0xE, 0xE, 0x1F, 0x11, 0x11, 0x11, 0x11},
                                   new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x10},
                                   new byte[]{ 0x3, 0x3, 0x7, 0x7, 0xF, 0xF, 0x1F, 0x1F},
                                   new byte[]{ 0x11, 0x11, 0x11, 0x1F, 0x11, 0x11, 0x11, 0x1F},
                                   new byte[]{ 0x18, 0x18, 0x1C, 0x1C, 0x1E, 0x1E, 0x1F, 0x1F},};

            byte[][] image2 = new byte[][]
                                 { new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                   new byte[]{ 0x0, 0x0, 0x0, 0xE, 0xE, 0xE, 0xE, 0xE},
                                   new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                   new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                   new byte[]{ 0xE, 0xE, 0xE, 0x0, 0xE, 0xE, 0xE, 0x0},
                                   new byte[]{ 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};
            #endregion

            while (!token.IsCancellationRequested)
            {
                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, image1[i]);
                }

                Thread.Sleep(CurrentStepTime);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, image2[i]);
                }

                Thread.Sleep(CurrentStepTime);
                if (token.IsCancellationRequested)
                    break;
            }

            lock (send_lock)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, image0[i]);
            }
        }
    }
}