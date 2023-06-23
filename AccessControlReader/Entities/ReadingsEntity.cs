using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlReader.Entities
{
    internal class Reading
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Required]
        public int Reading_ID { get; set; }
        public int Reader_ID { get; set; }
        public Reader Reader { get; set; }
        public int? User_ID { get; set; }
        public User? User { get; set; }
        public bool Access { get; set; }
        public string? Data { get; set; }

        [StringLength(12)]
        public UInt64? Card_ID_number { get; set; }
        public Card? Card { get; set; }
        public int? Card_ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
