using Iot.Device.CharacterLcd;

namespace AccessControlReader.LCD.Animation
{
    class ErrorAnimation : Animation
    {
        readonly ErrorTypeIcon[] errorTypeIcons;

        public ErrorAnimation(ErrorTypeIcon[] errorTypeIcons) : base(0)
        {
            this.errorTypeIcons = errorTypeIcons;
        }

        public override void StartAnimations(Lcd1602 Screen, object send_lock, bool loop, CancellationTokenSource token)
        {
            #region DeclareAnimationData
            byte[][] empty = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒▒│▒▒▒▒▒│▒▒▒▒▒║
            // ║█████│████▒│████▒║
            // ║██▒▒▒│█▒▒██│█▒▒██║
            // ║█▒▒▒▒│█▒▒▒█│█▒▒▒█║
            // ║█▒▒▒▒│█▒▒▒█│█▒▒▒█║
            // ║█▒▒▒▒│█▒▒▒█│█▒▒▒█║
            // ║█▒▒▒▒│█▒▒██│█▒▒██║
            // ║█▒▒▒▒│████▒│████▒║
            // ╟─────┼─────┼─────╢
            // ║████▒│██▒▒▒│██▒▒▒║
            // ║██▒▒▒│███▒▒│███▒▒║
            // ║█▒▒▒▒│█▒█▒▒│█▒█▒▒║
            // ║█▒▒▒▒│█▒██▒│█▒██▒║
            // ║█▒▒▒▒│█▒▒█▒│█▒▒█▒║
            // ║█▒▒▒▒│█▒▒██│█▒▒██║
            // ║██▒▒▒│█▒▒▒█│█▒▒▒█║
            // ║█████│█▒▒▒█│█▒▒▒█║
            // ╚═════╧═════╧═════╝
            byte[][] ERRicon = new byte[][]
                              { new byte[] { 0x0, 0x1F, 0x18, 0x10, 0x10, 0x10, 0x10, 0x10},
                                new byte[] {0x0, 0x1A, 0x13, 0x11, 0x11, 0x11, 0x13, 0x1E},
                                new byte[] { 0x0, 0x1A, 0x13, 0x11, 0x11, 0x11, 0x13, 0x1E},
                                new byte[] { 0x1E, 0x18, 0x10, 0x10, 0x10, 0x10, 0x18, 0x1F},
                                new byte[] { 0x18, 0x1C, 0x14, 0x16, 0x12, 0x13, 0x11, 0x11},
                                new byte[] { 0x18, 0x1C, 0x14, 0x16, 0x12, 0x13, 0x11, 0x11},};

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒▒│█████│▒▒▒▒▒║
            // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
            // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
            // ║▒▒▒▒▒│█▒▒▒█│▒▒▒▒▒║
            // ║▒▒▒▒▒│█████│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
            // ║█████│█████│█████║
            // ╟─────┼─────┼─────╢
            // ║█████│█████│█████║
            // ║▒▒█▒▒│▒▒▒▒▒│▒▒█▒▒║
            // ║▒▒█▒▒│▒▒▒▒▒│▒▒█▒▒║
            // ║█████│▒▒▒▒▒│█████║
            // ║█▒▒▒█│▒▒▒▒▒│█▒▒▒█║
            // ║█▒▒▒█│▒▒▒▒▒│█▒▒▒█║
            // ║█▒▒▒█│▒▒▒▒▒│█▒▒▒█║
            // ║█████│▒▒▒▒▒│█████║
            // ╚═════╧═════╧═════╝
            byte[][] NETWORKicon = new byte[][]
                              { new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F},
                                new byte[] { 0x1F, 0x11, 0x11, 0x11, 0x1F, 0x4, 0x4, 0x1F},
                                new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1F},
                                new byte[] { 0x1F, 0x4, 0x4, 0x1F, 0x11, 0x11, 0x11, 0x1F},
                                new byte[] { 0x1F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                new byte[] { 0x1F, 0x4, 0x4, 0x1F, 0x11, 0x11, 0x11, 0x1F}, };

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒▒│█████│▒▒▒▒▒║
            // ║▒▒▒██│▒▒▒▒▒│██▒▒▒║
            // ║▒██▒▒│▒▒▒▒▒│▒▒██▒║
            // ║█▒▒██│▒▒▒▒▒│██▒▒█║
            // ║█▒▒▒▒│█████│▒▒▒▒█║
            // ║█▒███│▒▒▒▒▒│█▒▒▒█║
            // ║█▒█▒▒│▒███▒│█▒▒▒█║
            // ║█▒█▒▒│█▒▒▒█│█▒▒▒█║
            // ╟─────┼─────┼─────╢
            // ║█▒███│█▒▒▒█│█▒▒▒█║
            // ║█▒▒▒█│█▒▒▒█│█▒▒▒█║
            // ║█▒▒▒█│█▒█▒█│█▒▒▒█║
            // ║█▒███│▒███▒│███▒█║
            // ║█▒▒▒▒│▒▒▒█▒│▒▒▒▒█║
            // ║▒██▒▒│▒▒▒▒█│▒▒██▒║
            // ║▒▒▒██│▒▒▒▒▒│██▒▒▒║
            // ║▒▒▒▒▒│█████│▒▒▒▒▒║
            // ╚═════╧═════╧═════╝
            byte[][] SQLicon = new byte[][]
                              { new byte[] {0x0, 0x3, 0xC, 0x13, 0x10, 0x17, 0x14, 0x14},
                                new byte[] {0x1F, 0x0, 0x0, 0x0, 0x1F, 0x0, 0xE, 0x11},
                                new byte[] {0x0, 0x18, 0x6, 0x19, 0x1, 0x11, 0x11, 0x11},
                                new byte[] {0x17, 0x11, 0x11, 0x17, 0x10, 0xC, 0x3, 0x0},
                                new byte[] {0x11, 0x11, 0x15, 0xE, 0x2, 0x1, 0x0, 0x1F},
                                new byte[] {0x11, 0x11, 0x11, 0x1D, 0x1, 0x6, 0x18, 0x0},};

            // ╔═════╤═════╤═════╗
            // ║█████│█████│▒▒▒▒▒║
            // ║█▒▒▒▒│▒▒▒▒█│▒▒▒▒▒║
            // ║█▒▒▒▒│▒▒▒▒█│█▒▒▒▒║
            // ║█▒▒▒▒│▒▒▒▒█│▒█▒▒▒║
            // ║█▒▒▒▒│▒▒▒▒█│▒▒█▒▒║
            // ║█▒▒▒▒│▒▒▒▒█│▒▒▒█▒║
            // ║█▒▒▒▒│▒▒▒▒█│█████║
            // ║█▒▒▒▒│▒▒▒▒▒│▒▒▒▒█║
            // ╟─────┼─────┼─────╢
            // ║█▒█▒█│██▒██│█▒▒▒█║
            // ║█▒█▒█│█▒█▒█│█▒▒▒█║
            // ║█▒▒█▒│█▒█▒█│█▒▒▒█║
            // ║█▒█▒█│█▒▒▒█│█▒▒▒█║
            // ║█▒█▒█│█▒▒▒█│███▒█║
            // ║█▒▒▒▒│▒▒▒▒▒│▒▒▒▒█║
            // ║█▒▒▒▒│▒▒▒▒▒│▒▒▒▒█║
            // ║█████│█████│█████║
            // ╚═════╧═════╧═════╝
            byte[][] XMLicon = new byte[][]
                               { new byte[] { 0x1F, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10},
                                 new byte[] { 0x1F, 0x1, 0x1, 0x1, 0x1, 0x1, 0x1, 0x0},
                                 new byte[] { 0x0, 0x0, 0x10, 0x8, 0x4, 0x2, 0x1F, 0x1},
                                 new byte[] { 0x15, 0x15, 0x12, 0x15, 0x15, 0x10, 0x10, 0x1F},
                                 new byte[] { 0x1B, 0x15, 0x15, 0x11, 0x11, 0x0, 0x0, 0x1F},
                                 new byte[] { 0x11, 0x11, 0x11, 0x11, 0x1D, 0x1, 0x1, 0x1F}, };

            // ╔═════╤═════╤═════╗
            // ║█▒▒▒▒│▒▒▒▒█│▒▒▒▒▒║
            // ║██▒▒▒│▒▒▒██│▒▒▒▒▒║
            // ║▒██▒▒│▒▒██▒│▒▒▒▒▒║
            // ║▒▒██▒│▒██▒▒│▒▒▒▒▒║
            // ║▒▒▒██│██▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒█│█▒▒▒▒│▒▒▒▒▒║
            // ║▒▒▒██│██▒▒▒│▒▒▒▒▒║
            // ║▒▒██▒│▒██▒▒│▒▒▒▒▒║
            // ╟─────┼─────┼─────╢
            // ║██▒▒▒│▒████│█████║
            // ║█▒▒▒▒│██▒▒▒│▒▒▒▒█║
            // ║▒▒▒▒▒│█▒▒▒▒│▒▒▒▒█║
            // ║▒▒▒▒▒│█▒▒██│▒▒▒▒█║
            // ║▒▒▒▒▒│█▒▒██│▒▒▒▒█║
            // ║▒▒▒▒▒│█▒▒▒▒│▒▒▒▒█║
            // ║▒▒▒▒▒│█▒▒▒▒│▒▒▒▒█║
            // ║▒▒▒▒▒│█████│█████║
            // ╚═════╧═════╧═════╝
            byte[][] RFIDicon = new byte[][]
                                { new byte[] { 0x10, 0x18, 0xC, 0x6, 0x3, 0x1, 0x3, 0x6},
                                  new byte[] { 0x1, 0x3, 0x6, 0xC, 0x18, 0x10, 0x18, 0xC},
                                  new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                  new byte[] { 0x18, 0x10, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                  new byte[] { 0xF, 0x18, 0x10, 0x13, 0x13, 0x10, 0x10, 0x1F},
                                  new byte[] { 0x1F, 0x1, 0x1, 0x1, 0x1, 0x1, 0x1, 0x1F},};

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒█│█▒█▒█│█▒▒▒▒║
            // ║▒▒▒▒█│█▒█▒█│█▒▒▒▒║
            // ║▒▒███│█████│███▒▒║
            // ║███▒▒│▒▒▒▒▒│▒▒███║
            // ║▒▒█▒▒│▒████│█▒█▒▒║
            // ║███▒▒│██▒██│█▒███║
            // ║▒▒█▒▒│██▒██│█▒█▒▒║
            // ║███▒█│██▒██│█▒███║
            // ╟─────┼─────┼─────╢
            // ║███▒█│██▒██│█▒███║
            // ║▒▒█▒█│█████│█▒█▒▒║
            // ║███▒█│██▒██│█▒███║
            // ║▒▒█▒█│█████│█▒█▒▒║
            // ║███▒▒│▒▒▒▒▒│▒▒███║
            // ║▒▒███│█████│███▒▒║
            // ║▒▒▒▒█│█▒█▒█│█▒▒▒▒║
            // ║▒▒▒▒█│█▒█▒█│█▒▒▒▒║
            // ╚═════╧═════╧═════╝
            byte[][] HARDWAREicon = new byte[][]
                                    { new byte[] { 0x1, 0x1, 0x7, 0x1C, 0x4, 0x1C, 0x4, 0x1D},
                                      new byte[] { 0x15, 0x15, 0x1F, 0x0, 0xF, 0x1B, 0x1B, 0x1B},
                                      new byte[] { 0x10, 0x10, 0x1C, 0x7, 0x14, 0x17, 0x14, 0x17},
                                      new byte[] { 0x1D, 0x5, 0x1D, 0x5, 0x1C, 0x7, 0x1, 0x1},
                                      new byte[] { 0x1B, 0x1F, 0x1B, 0x1F, 0x0, 0x1F, 0x15, 0x15},
                                      new byte[] { 0x17, 0x14, 0x17, 0x14, 0x7, 0x1C, 0x10, 0x10},};

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒▒│▒██▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│█▒█▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
            // ║▒▒▒▒█│▒▒█▒▒│▒▒▒▒▒║
            // ║████▒│▒▒█▒█│▒▒▒▒▒║
            // ║█▒▒█▒│▒▒██▒│▒▒▒▒▒║
            // ║█▒▒█▒│▒▒██▒│▒▒▒▒▒║
            // ║█▒▒█▒│▒▒█▒▒│█▒▒▒▒║
            // ╟─────┼─────┼─────╢
            // ║█▒▒█▒│▒█▒▒█│▒█▒▒▒║
            // ║█▒▒█▒│▒█▒▒█│▒█▒▒▒║
            // ║█▒▒█▒│█▒▒▒█│▒▒█▒▒║
            // ║████▒│█▒▒▒█│▒▒█▒▒║
            // ║▒▒▒▒█│▒▒▒▒▒│▒▒▒█▒║
            // ║▒▒▒▒▒│▒▒▒▒█│▒▒▒█▒║
            // ║▒▒▒▒█│▒▒▒▒▒│▒▒▒▒█║
            // ║▒▒▒▒█│█████│█████║
            // ╚═════╧═════╧═════╝
            byte[][] SPEKERicon = new byte[][]
                                    { new byte[] { 0x0, 0x0, 0x0, 0x1, 0x1E, 0x12, 0x12, 0x12},
                                      new byte[] { 0xC, 0x14, 0x4, 0x4, 0x5, 0x6, 0x6, 0x4},
                                      new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10},
                                      new byte[] { 0x12, 0x12, 0x12, 0x1E, 0x1, 0x0, 0x1, 0x1},
                                      new byte[] { 0x9, 0x9, 0x11, 0x11, 0x0, 0x1, 0x0, 0x1F},
                                      new byte[] { 0x8, 0x8, 0x4, 0x4, 0x2, 0x2, 0x1, 0x1F}, };

            // ╔═════╤═════╤═════╗
            // ║▒▒▒▒▒│▒▒▒▒█│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒▒█▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
            // ║▒▒██▒│▒██▒▒│▒▒▒▒▒║
            // ║▒▒██▒│▒█▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒█▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│██▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│██▒▒▒│▒▒▒▒▒║
            // ╟─────┼─────┼─────╢
            // ║▒▒▒▒▒│██▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│██▒▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒█▒▒▒│▒▒▒▒▒║
            // ║▒▒██▒│▒█▒▒▒│▒▒▒▒▒║
            // ║▒▒██▒│▒██▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒█▒▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒▒█▒│▒▒▒▒▒║
            // ║▒▒▒▒▒│▒▒▒▒█│▒▒▒▒▒║
            // ╚═════╧═════╧═════╝
            byte[][] INTERNALicon = new byte[][]
                                 { new byte[] {0x0, 0x0, 0x0, 0x6, 0x6, 0x0, 0x0, 0x0},
                                   new byte[] {0x1, 0x2, 0x4, 0xC, 0x8, 0x8, 0x18, 0x18},
                                   new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},
                                   new byte[] {0x0, 0x0, 0x0, 0x6, 0x6, 0x0, 0x0, 0x0},
                                   new byte[] {0x18, 0x18, 0x8, 0x8, 0xC, 0x4, 0x2, 0x1},
                                   new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0},};
            #endregion

            int CurrentIconInt = 0;
            ErrorTypeIcon? CurrentIconType = null;

            while (!token.IsCancellationRequested)
            {
                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, empty[i]);
                }

                if (errorTypeIcons is not null)
                {
                    if (errorTypeIcons.Length > CurrentIconInt)
                        CurrentIconInt++;
                    else
                        CurrentIconInt = 0;

                    CurrentIconType = errorTypeIcons[CurrentIconInt];
                }

                Thread.Sleep(100);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    switch (CurrentIconType)
                    {
                        case ErrorTypeIcon.LAN:

                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, NETWORKicon[i]);
                            break;

                        case ErrorTypeIcon.SQL:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, SQLicon[i]);
                            break;

                        case ErrorTypeIcon.RFID:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, RFIDicon[i]);
                            break;

                        case ErrorTypeIcon.XML:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, XMLicon[i]);
                            break;

                        case ErrorTypeIcon.Hardware:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, HARDWAREicon[i]);
                            break;

                        case ErrorTypeIcon.Internal:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, INTERNALicon[i]);
                            break;

                        case ErrorTypeIcon.Speaker:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, SPEKERicon[i]);
                            break;

                        case null:
                            for (int i = 0; i < 6; i++)
                                Screen.CreateCustomCharacter(i, ERRicon[i]);
                            break;
                    }
                }

                Thread.Sleep(900);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, empty[i]);
                }

                Thread.Sleep(50);
                if (token.IsCancellationRequested)
                    break;

                lock (send_lock)
                {
                    for (int i = 0; i < 6; i++)
                        Screen.CreateCustomCharacter(i, ERRicon[i]);
                }

                Thread.Sleep(450);
                if (token.IsCancellationRequested)
                    break;
            }
        }
    }
}