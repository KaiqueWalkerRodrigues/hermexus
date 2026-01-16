using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("companies")]
    public class Company : BaseEntity
    {
        [Column("is_active")]
        public bool Is_active { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("legal_name")]
        public string Legal_name { get; set; } = string.Empty;
        [Column("document_number")]
        public string Document_number { get; set; } = string.Empty;
    }
}
