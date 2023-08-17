using System.Xml;
using System.Xml.Linq;

namespace AccessControlReader
{
    internal sealed class Configurator
    {
        readonly private string FilePath;
        private const string FileName = "AccessControl_ConfigFile.xml";
        private readonly IEnumerable<XElement> XElements;

        public static ErrorEvent errorEvent;

        public Configurator()
        {
            string DesktopPath = null;
            try
            {
                DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            catch (PlatformNotSupportedException)
            {
                errorEvent.Invoke(GetType(), "Platform not supported", 10, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Internal });
            }
            catch (Exception ex)
            {
                errorEvent.Invoke(GetType(), ex.ToString(), 11, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.Internal });
            }

            FilePath = Path.Combine(DesktopPath, FileName);
            XDocument xdoc = null;

            if (File.Exists(FilePath))
            { 
                try
                {
                    TextReader ConfTR = new StreamReader(FilePath);
                    xdoc = XDocument.Load(ConfTR, new LoadOptions());
                }
                catch (Exception ex)
                {
                    errorEvent.Invoke(GetType(), ex.ToString(), 14, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Internal });
                }
            }
            else
            {
                TextReader DefConfTR = null;
                try
                {
                    DefConfTR = new StreamReader("DefaultConfigFile.xml");
                }
                catch (Exception ex)
                {
                    errorEvent(this.GetType(), ex.Message, 12, ErrorImportant.Critical, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Internal });
                    return;
                }

                try 
                { 
                    xdoc = XDocument.Load(DefConfTR);
                    foreach (XElement file in xdoc.Element("ACConfig").Element("Noises").Elements())
                    {
                        file.Value = Path.Combine("Noises", file.Value);
                    }
                }
                catch (XmlException ex)
                {
                    errorEvent(this.GetType(), ex.Message, 16, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML });
                }
                catch (Exception ex)
                {
                    errorEvent(this.GetType(), ex.Message, 15, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Internal });
                }

                try
                {
                    File.WriteAllText(FilePath, xdoc.ToString());
                }
                catch (Exception ex)
                {
                    errorEvent(this.GetType(), ex.Message, 13, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Internal });
                }
            }

            try
            {
                XElements = xdoc.Element("ACConfig").Elements().Where(t => t.Name != "Devices");
                XElements = XElements.Concat(xdoc.Element("ACConfig").Element("Devices").Elements());
            }
            catch (Exception ex)
            {
                errorEvent(GetType(), ex.Message, 17, ErrorImportant.Warning, new ErrorTypeIcon[] {ErrorTypeIcon.XML} );
            }
        }

        public XElement XElementOfConfig(string PartToConf)
        {
            if (!XElements.Any(XE => XE.Name == PartToConf))
            {
                errorEvent(this.GetType(), "No key value", 18, ErrorImportant.Warning, new ErrorTypeIcon[] { ErrorTypeIcon.XML, ErrorTypeIcon.Internal });
                return null;
            }

            return XElements.Where(XE => XE.Name.LocalName == PartToConf)?.First();
        }
    }
}