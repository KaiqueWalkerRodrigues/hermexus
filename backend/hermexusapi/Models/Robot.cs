using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("robots")]
    public class Robot : BaseEntity
    {
        [Column("is_active")]
        public bool Is_active { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
