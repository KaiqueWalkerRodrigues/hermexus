
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("sectors")]
    public class Sector : BaseEntity
    {
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("companies_id")]
        public long CompanyId { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
    }
}
