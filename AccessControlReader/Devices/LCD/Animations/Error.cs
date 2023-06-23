namespace AccessControlReader.LCD.Animation
{
    internal partial class Animations
    {
        public static void ErrorAnimation(object objects)
        {
            if (objects is not object[] objectsArray || objectsArray.Length != 2 || objectsArray.Length != 3)
            {
                throw new ArgumentException("objects must be initialized and its size must be 2 or 3");
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

            ErrorTypeIcon? errorTypeIcon;
            if (objectsArray.Length >= 3)
            {
                try
                {
                    errorTypeIcon = (ErrorTypeIcon)objectsArray[2];
                }
                catch
                {
                    throw new ArgumentException("objects[2] must be the ErrorTypeIcon");
                }
            }
            else
                errorTypeIcon = null;


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

            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, empty[i]);

                Thread.Sleep(50);

                if (token.IsCancellationRequested)
                    break;

                switch(errorTypeIcon) //TODO dodać resztę
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

                    case null:
                        break;
                }

                Thread.Sleep(450);

                if (token.IsCancellationRequested)
                    break;

                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, empty[i]);

                Thread.Sleep(50);

                for (int i = 0; i < 6; i++)
                    Screen.CreateCustomCharacter(i, ERRicon[i]);

                Thread.Sleep(450);

                if (token.IsCancellationRequested)
                    break;
            }
        }
    }
}