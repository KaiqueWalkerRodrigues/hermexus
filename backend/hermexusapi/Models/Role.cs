using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("roles")]
    public class Role : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
    }
}
