namespace hermexusapi.DTO.V1
{
    public class TokenDTO
    {
        public TokenDTO() { }

        public TokenDTO(
            bool authenticated,
            string created,
            string expiration,
            string accessToken,
            string refreshToken
            )
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            Access_token = accessToken;
            Refresh_token = refreshToken;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
    }
}
