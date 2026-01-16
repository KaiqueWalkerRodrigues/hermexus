using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("robots")]
    public class RobotDTO : BaseEntity, ISupportsHypermedia
    {
        [Column("is_active")]
        public bool? Is_active { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
