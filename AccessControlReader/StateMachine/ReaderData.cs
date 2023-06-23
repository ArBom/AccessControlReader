using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlReader.StateMachine
{
    public struct ReaderData
    {
        //public int Reader_ID; //potrzebne w QSL
        //public string MACaddr; //przechowywane jedynie  w inicie
        //public short Tier; //potrzebne w SQL
        public string? ToShow; //trzymane w stanach
        //public string? Comment; //w zasadzie zbedne
        public bool IsActive; //trzymane w stanie
        public int? ErrorNo; //ustawiane w stanach
    }
}
