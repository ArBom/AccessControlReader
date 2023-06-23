using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlReader.Entities
{
    internal class Card
    {
        /// <summary>
        /// Primary key of card at SQL
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Required]
        public int Card_ID { get; set; }

        /// <summary>
        /// Unique NFC ID Number which can be read with RFID reader
        /// </summary>
        public UInt32 Card_ID_number { get; set; }

        /// <summary>
        /// Primary key of user consociated with card
        /// </summary>
        public int User_ID { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Text written on card surface
        /// </summary>
        [Column(TypeName ="varchar(25)")]
        public string Card_UID { get; set; }

        public short Tier { get; set; }

        public string Comment { get; set; }

        public IEnumerable<Reading> Readings { get; set; }
    }
}
