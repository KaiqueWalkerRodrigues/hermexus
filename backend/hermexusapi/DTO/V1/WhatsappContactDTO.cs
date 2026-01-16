using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("whatsapp_contacts")]
    public class WhatsappContactDTO : BaseEntity, ISupportsHypermedia
    {
        public string? Name { get; set; }
        public string? Phone_number { get; set; }
        public string? Wa_id { get; set; }
        public List<HypermediaLink> Links { get; set; } = [];
    }
}
