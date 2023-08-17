using System.Xml.Linq;
using NetCoreAudio;

namespace AccessControlReader
{
    sealed class Noises
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
                //try to play error noise
                try
                {
                    string errorNoisePath = Path.Combine("Noises", "mixkit-948.wav");
                    Task pe = player.Play(errorNoisePath);
                    Task.WhenAny(pe);
                }
                catch { } //There is no sense to overwrite more serious problem

                //There is no sense to try play noises in the future
                player = null;

                errorEvent(GetType(), "Noises: Config is null", 80, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Speaker });
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
                    errorEvent(GetType(), "Cant open noise file", 83, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Speaker });
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
                        errorEvent(GetType(), "Wrong format of noise file", 84, ErrorImportant.Info, new ErrorTypeIcon[] { ErrorTypeIcon.Speaker });
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
            if (player is null)
                return;

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
                Task a = player.Play(NoisePath);
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