using RestWithASPNET10Erudio.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace hermexusapi.Models
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("Is_active")]
        public bool Is_active { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime? Created_at { get; set; }

        [Column("updated_at")]
        public DateTime? Updated_at { get; set; }

        [Column("deleted_at")]
        public DateTime? Deleted_at { get; set; }
    }
}
