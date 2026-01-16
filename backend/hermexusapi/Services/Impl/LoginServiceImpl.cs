using hermexusapi.Auth.Config;
using hermexusapi.Auth.Contract;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using Mapster;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace hermexusapi.Services.Impl
{
    public class LoginServiceImpl : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenService;
        private readonly TokenConfig _configurations;
        public LoginServiceImpl(
            IUserService userService,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenService,
            TokenConfig configurations
            )
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configurations = configurations;
        }

        // Verification by username and password
        public TokenDTO? ValidateCredentials(SignInDTO userDto)
        {
            var user = _userService
                .FindByUsername(userDto.Username);

            if (user == null) return null;
            if (!_passwordHasher
                .Verify(userDto.Password, user.Password))
                return null;
            if(!user.Is_active) return null;

            return GenerateToken(user);
        }

        // Verification by refresh token
        public TokenDTO ValidateCredentials(TokenDTO token)
        {
            var principal = _tokenService
                .GetPrincipalFromExpiredToken(token.Access_token);
            var username = principal.Identity?.Name;

            var user = _userService.FindByUsername(username);
            if (user == null
              || user.Refresh_token != token.Refresh_token
              || user.Refresh_token_expiry_time <= DateTime.Now)
            {
                return null;
            }
            else
            {
                return GenerateToken(user, principal.Claims);
            }
        }

        public bool RevokeToken(string username)
        {
            var user = _userService.FindByUsername(username);

            if (user == null) return false;

            user.Refresh_token = null;

            var userDto = user.Adapt<UserDTO>();

            _userService.Update(userDto);

            return true;
        }

        private TokenDTO GenerateToken(
            User user,
            IEnumerable<Claim>? existingClaims = null
            )
        {
            var claims = existingClaims?.ToList() ??
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimTypes.Name, user.Username)
            ];

            var accessToken = _tokenService
                .GenerateAccessToken(claims);

            var refreshToken = _tokenService
                .GenerateRefreshToken();

            user.Refresh_token = refreshToken;
            user.Refresh_token_expiry_time = DateTime.UtcNow
                .AddDays(_configurations.Days_to_expiry);

            var userDto = user.Adapt<UserDTO>();

            _userService.Update(userDto);

            var createdDate = DateTime.UtcNow;
            var expirationDate = createdDate
                .AddMinutes(_configurations.Minutes);

            return new TokenDTO
            {
                Authenticated = true,
                Created = createdDate.ToString(DATE_FORMAT),
                Expiration = expirationDate.ToString(DATE_FORMAT),
                Access_token = accessToken,
                Refresh_token = refreshToken
            };
        }
    }
}
