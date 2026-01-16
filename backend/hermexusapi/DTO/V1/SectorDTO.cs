using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("sectors")]
    public class SectorDTO : BaseEntity, ISupportsHypermedia
    {
        public bool Is_active { get; set; }
        public long? Company_id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
