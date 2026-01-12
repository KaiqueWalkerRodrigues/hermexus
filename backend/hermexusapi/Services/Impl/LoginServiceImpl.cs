using hermexusapi.Auth.Config;
using hermexusapi.Auth.Contract;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace hermexusapi.Services.Impl
{
    public class LoginServiceImpl : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private readonly IUserAuthService _userAuthService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenService;
        private readonly TokenConfig _configurations;
        public LoginServiceImpl(
            IUserAuthService userAuthService,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenService,
            TokenConfig configurations
            )
        {
            _userAuthService = userAuthService;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configurations = configurations;
        }
        public TokenDTO ValidateCredentials(UserDTO userDto)
        {
            var user = _userAuthService
                .FindByUsername(userDto.Username);

            if (user == null) return null;
            if (!_passwordHasher
                .Verify(userDto.Password, user.Password))
                return null;

            return GenerateToken(user);
        }

        public TokenDTO ValidateCredentials(TokenDTO token)
        {
            var principal = _tokenService
                .GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;

            var user = _userAuthService.FindByUsername(username);
            if (user == null
              || user.RefreshToken != token.RefreshToken
              || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null;
            }
            else
            {
                return GenerateToken(user, principal.Claims);
            }
        }
        public AccountCredentialsDTO Create(AccountCredentialsDTO dto)
        {
            var user = _userAuthService
                .Create(dto);
            return new AccountCredentialsDTO
            {
                Is_active = user.Is_active.ToString(), // Convert bool to string
                Username = user.Username,
                Name = user.Name,
                Password = "************"
            };
        }

        public bool RevokeToken(string username)
        {
            return _userAuthService
                .RevokeToken(username);
        }

        private TokenDTO GenerateToken(
            User user,
            IEnumerable<Claim>? existingClaims = null
            )
        {
            var claims = existingClaims?.ToList() ??
            //new List<Claim>
            [
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
               new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            ];

            var accessToken = _tokenService
                .GenerateAccessToken(claims);

            var refreshToken = _tokenService
                .GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow
                .AddDays(_configurations.DaysToExpiry);

            _userAuthService.Update(user);

            var createdDate = DateTime.Now;
            var expirationDate = createdDate
                .AddMinutes(_configurations.Minutes);

            return new TokenDTO
            {
                Authenticated = true,
                Created = createdDate.ToString(DATE_FORMAT),
                Expiration = expirationDate.ToString(DATE_FORMAT),
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
