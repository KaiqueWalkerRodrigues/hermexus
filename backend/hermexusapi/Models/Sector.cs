
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("sectors")]
    public class Sector : BaseEntity
    {
        [Column("is_active")]
        public bool Is_active { get; set; }
        [Column("company_id")]
        public long Company_id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
    }
}
