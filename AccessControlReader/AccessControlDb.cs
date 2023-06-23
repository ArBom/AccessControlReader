using AccessControlReader.Entities;
using AccessControlReader.StateMachine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccessControlReader
{
    internal class AccessControlDb
    {
        private readonly AccessControlDbContext ACDbC;
        public StateEvent DataBaseEvent;
        private int? ThisReaderID;
        private int? ThisReaderTier;

        public AccessControlDb(XElement Config)
        {
            if (Config is null)
            {
                throw new ArgumentException(); //TODO
            }

            string ConnectionString = Config.Element("ConnectionString")?.Value;

            if (String.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentException(); //TODO
            }

            if (ConnectionString == "❗❗❗ INSERT IT HERE ❗❗❗")
            {
                Console.WriteLine("Change the connection string at file on desktop");
                throw new ArgumentException(); //TODO
            }

            ThisReaderID = null;
            ThisReaderTier = null;

            ACDbC = new AccessControlDbContext(ConnectionString);
        }

        private bool CanConnect()
        {
            bool CanConnect = ACDbC.Database.CanConnect();
            if(!CanConnect)
            {
                //TODP info o błędzie
                Console.WriteLine("Nie można połączyć się z SQL");
            }

            return CanConnect;
        }

        public void CheckCard(UInt32 Id)
        {
            Console.WriteLine("Pobieram dane o karcie " + Id);

            if(!CanConnect())
            {
                return;
            }

            bool CardIsKnown = ACDbC.CardItems.Any(x => x.Card_ID_number == Id);
            if (!CardIsKnown)
            {
                Console.WriteLine("Karta nie figuruje w bazie");
                INVALIDCardProc(Id, null, null);
                return;
            }

            //Pobranie danych o karcie
            var sqlCard = ACDbC.CardItems.Where(x => x.Card_ID_number == Id)?.Select(x => new { x.Card_ID, x.Tier, x.User_ID})?.First();

            Console.WriteLine("Pobrałem dane o karcie " + Id);

            //sprawdzenie czy karta jest zapisana w sql, uzyskanie tier i userid

            if (sqlCard.Tier < ThisReaderTier)
            {
                //za słaba karta
                Console.WriteLine("Karta ma za niski poziom");
                INVALIDCardProc(Id, sqlCard.Card_ID, sqlCard.User_ID);
                return;
            }

            //jesli istnieje i odpowiedni tier
            ValidCardProc(sqlCard.Card_ID, sqlCard.User_ID);
        }

        private void INVALIDCardProc(UInt64? Card_ID_number, int? Card_ID, int? User_ID)
        {
            DataBaseEvent?.Invoke(EventType.WrongCard, null);

            //Zapis do DB
            Reading ReadingToAdd = new()
            {
                Reader_ID = ThisReaderID.Value,
                User_ID = User_ID,
                Access = false,
                Data = null,
                Card_ID = Card_ID,
                Card_ID_number = Card_ID_number,
            };

            var newReading = ACDbC.Set<Reading>();
            newReading.Add(ReadingToAdd);

            int addingres = ACDbC.SaveChanges();
        }

        private void ValidCardProc(int Card_ID, int User_ID)
        {
            var sqlUser = ACDbC.UsersItems.Where(x => x.User_ID == User_ID)?.First();

            //Zapis do DB
            Reading ReadingToAdd = new()
            {
                Reader_ID = ThisReaderID.Value,
                User_ID = User_ID,
                Access = true,
                Data = null,
                Card_ID = Card_ID,
            };

            var newReading = ACDbC.Set<Reading>();
            newReading.Add(ReadingToAdd);

            int addingres = ACDbC.SaveChanges();
            string ToShow = sqlUser.FirstName.ToArray()[0] + @". " + sqlUser.LastName; 
            DataBaseEvent?.Invoke(EventType.CorrectCard, ToShow);
        }

        public ReaderData? UpdateReader(string MacAddr)
        {
            Console.WriteLine("UpdateReader at AccessControlDb MAC: " + MacAddr);

            if (!CanConnect())
                return null;

            Console.WriteLine("UpdateReader at AccessControlDb can connect.");

            bool ReaderExist = ACDbC.ReadersItems.Any(x => x.MACaddr == MacAddr);

            Reader ReaderToRet = null;

            if (!ReaderExist) 
            {
                Console.WriteLine("ReaderToRet == null");
                DataBaseEvent?.Invoke(EventType.MainState_Unconfig, null);
                ReaderToRet = AddItself(MacAddr);
            }

            ReaderToRet = ACDbC.ReadersItems.Where(x => x.MACaddr == MacAddr)?.First();

            if (ReaderToRet is null)
                return null;

            ThisReaderID = ReaderToRet.Reader_ID;
            ThisReaderTier = ReaderToRet.Tier;

            UpdateBlReStatus(ReaderToRet);

            ReaderData Toret = new(){ ErrorNo = ReaderToRet.ErrorNo, IsActive = ReaderToRet.IsActive, ToShow = ReaderToRet.ToShow };
            return Toret;
        }

        public ReaderData? UpdateReader()
        {
            if (ThisReaderID is null)
            {
                Console.WriteLine("UpdateReader() inside AccessControlDb: ThisReaderID is null");
                //TODO poinformowanie o błędzie 
                return null;
            }

            Console.WriteLine("UpdateReader() inside AccessControlDb: ThisReaderID=" + ThisReaderID.ToString());

            if (!CanConnect())
                return null;

            Reader ReaderInSQL = ACDbC.ReadersItems.Where(x => x.Reader_ID == ThisReaderID.Value)?.First();

            if (ReaderInSQL is null)
            {
                Console.WriteLine("UpdateReader() inside AccessControlDb: ReaderInSQL is null");
                //TODO poinformowanie o błędzie 
                return null;
            }

            ThisReaderTier = ReaderInSQL.Tier;

            UpdateBlReStatus(ReaderInSQL);

            ReaderData Toret = new() { IsActive = ReaderInSQL.IsActive, ToShow = ReaderInSQL.ToShow };
            Console.WriteLine("UpdateReader() inside AccessControlDb: returned value IsActive: " + ReaderInSQL.IsActive + " ToShow: " + ReaderInSQL.ToShow);
            return Toret;
        }

        private Reader AddItself(string MacAddr)
        {
            Console.WriteLine("Adding itself");

            if (!CanConnect())
                return null;

            Reader ReaderToAdd = new()
            {
                MACaddr = MacAddr,
                Tier = -1,
                ToShow = "Config me, please.",
                Comment = "Device connected at " + DateTime.Now,
                IsActive = true,
                ErrorNo = 1,
            };

            var newReader = ACDbC.Set<Reader>();
            newReader.Add(ReaderToAdd);
            Console.WriteLine("change saving");
            int addingres = ACDbC.SaveChanges();
            Console.WriteLine("Adding itself save change result: "+ addingres);

            ThisReaderID = ReaderToAdd.Reader_ID;
            ThisReaderTier = ReaderToAdd.Tier;

            return ReaderToAdd;
        }

        private void UpdateBlReStatus(Reader reader)
        {
            //(ErrorNo == 1) => reader is added to database, but it is unconfig 
            if (reader.ErrorNo == 1)
            {
                DataBaseEvent?.Invoke(EventType.MainState_Unconfig, null);
                return;
            }

            if (reader.IsActive)
            {
                DataBaseEvent?.Invoke(EventType.MainState_Reading, reader.ToShow);
            }
            else
            {
                DataBaseEvent?.Invoke(EventType.MainState_Blocked, reader.ToShow);
            }
        }
    }
}
