using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("whatsapp_contacts")]
    public class WhatsappContact : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("phone_number")]
        public string Phone_number { get; set; } = string.Empty;
        [Column("wa_id")]
        public string Wa_id { get; set; } = string.Empty;
    }
}
