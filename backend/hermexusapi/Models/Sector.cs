
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("sectors")]
    public class Sector : BaseEntity
    {
        [Column("is_active")]
        public bool Is_active { get; set; }
        [Column("companies_id")]
        public long Companies_Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Descripition { get; set; } = string.Empty;
    }
}
