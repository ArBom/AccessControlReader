using System.Xml.Linq;
using NetCoreAudio;

namespace AccessControlReader
{
    public class Noises
    {
        private readonly Dictionary<NoiseType, string> NoisesPath;
        private readonly Player player;

        public static ErrorEvent errorEvent;

        public Noises(XElement Config)
        {
            player = new();
            NoisesPath = new();

            if(Config is null)
            {
                string errorNoisePath = Path.Combine("Noises", "mixkit-948.wav");
                Task pe = player.Play(errorNoisePath);
                Task.WhenAny(pe);
                player = null;

                //TODO poinformowanie o bledzie.

                return;
            }

            foreach (string TypeOfNoise in Enum.GetNames(typeof(NoiseType)))
            {
                string path;
                try
                {
                    path = Config.Element(TypeOfNoise).Value;
                    var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    file.Close();                   
                }
                catch
                {
                    //TODO jakiś warning
                    NoisesPath.Add(Enum.Parse<NoiseType>(TypeOfNoise), null);
                    continue;
                }

                switch(Path.GetExtension(path)?.ToLower())
                {
                    case ".wav":
                    case ".mp3":
                        NoisesPath.Add(Enum.Parse<NoiseType>(TypeOfNoise), path);
                        break;

                    case null:
                    default:
                        //TODO jakiś warning
                        NoisesPath.Add(Enum.Parse<NoiseType>(TypeOfNoise), null);
                        break;
                }
            }
        }

        public void PlayAsync(NoiseType noiseType, params CancellationTokenSource[] CancelationObj)
        {
            Task task = new(() => { Play(noiseType, CancelationObj); });
            task.Start();
        }

        private void Play(NoiseType noiseType, params CancellationTokenSource[] CancelationObj)
        {
            CancellationTokenSource token;

            if (CancelationObj.Length != 0)
                token = CancelationObj[0];
            else
            {
                token = new CancellationTokenSource();
                token.Cancel();
            }

            string NoisePath = NoisesPath[noiseType];
            if (String.IsNullOrEmpty(NoisePath))
                return;

            do
            {
                Task a = player?.Play(NoisePath);
                Task.WaitAny(a);
                Thread.Sleep(1000);
            }
            while (!token.IsCancellationRequested);
        }

        public void Stop() 
        {
            if (player == null)
                return;

            if (player.Playing)
                player.Stop();
        }
    }
}
