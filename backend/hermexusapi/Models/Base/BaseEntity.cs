using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("created_at")]
        public DateTime? Created_at { get; set; }

        [Column("updated_at")]
        public DateTime? Updated_at { get; set; }

        [Column("deleted_at")]
        public DateTime? Deleted_at { get; set; }
    }
}