using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlReader.Entities
{
    internal class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Required]
        public int User_ID { get; set; }

        [MaxLength(15)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();
        public IEnumerable<Reading> Readings { get; set; }
    }
}
