using hermexusapi.Hypermedia;
using hermexusapi.Hypermedia.Abstract;
using hermexusapi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.DTO.V1
{
    [Table("users")]
    public class UserDTO : BaseEntity, ISupportsHypermedia
    {
        [Column("username")]
        public string? Username { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("is_active")]
        public bool Is_active { get; set; }

        [Column("refresh_token")]
        public string? Refresh_token { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? Refresh_token_expiry_time { get; set; }

        public List<HypermediaLink> Links { get; set; } = [];
    }
}
