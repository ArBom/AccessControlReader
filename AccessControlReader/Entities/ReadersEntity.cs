using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlReader.Entities
{
    internal class Reader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Required]
        public int Reader_ID { get; set; }

        [StringLength(12)]
        public string MACaddr { get; set; }

        public short Tier { get; set; }

        [MaxLength(34)]
        public string? ToShow { get; set; }

        [MaxLength(50)]
        public string? Comment { get; set; }

        public bool IsActive { get; set; }
        public int? ErrorNo { get; set; }

        public IEnumerable<Reading> Readings { get; set; }
    }
}
