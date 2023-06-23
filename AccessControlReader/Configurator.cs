using AccessControlReader.StateMachine;
using System.Xml.Linq;

namespace AccessControlReader
{
    internal class Configurator
    {
        readonly private string FilePath;
        private const string FileName = "AccessControl_ConfigFile.xml";
        private readonly IEnumerable<XElement> XElements;

        public static ErrorEvent errorEvent;

        public XElement XElementOfConfig (string PartToConf)
        {

            if (!XElements.Any(XE => XE.Name == PartToConf))
            {
                return null;
                //throw new Exception();
                //powinien zwrocic null zeby całość mogla zakomunikowac o bledzie
            }

            return XElements.Where(XE => XE.Name.LocalName == PartToConf)?.First();
        }

        public Configurator()
        {
            string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FilePath = Path.Combine(DesktopPath, FileName);
            XDocument xdoc = null;

            if (File.Exists(FilePath)) 
            { 
                try
                {
                    TextReader ConfTR = new StreamReader(FilePath);
                    xdoc = XDocument.Load(ConfTR, new LoadOptions());
                }
                catch
                {
                    throw new Exception();
                }
            }
            else
            {
                try
                {
                    TextReader DefConfTR = new StreamReader("DefaultConfigFile.xml");
                    xdoc = XDocument.Load(DefConfTR);
                    foreach (XElement file in xdoc.Element("ACConfig").Element("Noises").Elements())
                    {
                        file.Value = Path.Combine("Noises", file.Value);
                    }
                }
                catch
                {
                    throw new Exception();
                }

                File.WriteAllText(FilePath, xdoc.ToString());
            }

            try
            {
                XElements = xdoc?.Element("ACConfig")?.Elements()?.Where(t => t.Name != "Devices");
                XElements = XElements.Concat(xdoc?.Element("ACConfig")?.Element("Devices")?.Elements());
            }
            catch 
            { }
        }
    }
}
