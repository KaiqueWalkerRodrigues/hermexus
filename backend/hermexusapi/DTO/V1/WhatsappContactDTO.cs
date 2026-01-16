using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("whatsapp_contacts")]
    public class WhatsappContactDTO : BaseEntity, ISupportsHypermedia
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("phone_number")]
        public string? Phone_number { get; set; }
        [Column("wa_id")]
        public string? Wa_id { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
