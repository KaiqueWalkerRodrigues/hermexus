using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("companies")]
    public class CompanyDTO : BaseEntity, ISupportsHypermedia
    {
        [Column("is_active")]
        public bool Is_active { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("legal_name")]
        public string? Legal_name { get; set; }
        [Column("document_number")]
        public string? Document_number { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
