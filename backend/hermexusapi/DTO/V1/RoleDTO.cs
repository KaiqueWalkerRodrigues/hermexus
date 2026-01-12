using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("roles")]
    public class RoleDTO : BaseEntity, ISupportsHypermedia
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
