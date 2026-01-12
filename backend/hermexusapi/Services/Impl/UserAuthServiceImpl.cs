using hermexusapi.Auth.Contract;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.Repositories;
using hermexusapi.Services;

namespace hermexusapi.Services.Impl
{
    public class UserAuthServiceImpl(
        IUserRepository repository,
        IPasswordHasher passwordHasher
        ) : IUserAuthService
    {
        private readonly IUserRepository _repository = repository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public User FindByUsername(string username)
        {
            return _repository.FindByUsername(username);
        }
        public User Create(AccountCredentialsDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = new User
            {
                Is_active = bool.TryParse(dto.Is_active, out var isActive) && isActive,
                Username = dto.Username,
                Name = dto.Name,
                Password = _passwordHasher.Hash(dto.Password),
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = null
            };
            return _repository.Create(entity);
        }

        public bool RevokeToken(string username)
        {
            var user = _repository.FindByUsername(username);
            if (user == null) return false;
            user.RefreshToken = null;
            _repository.Update(user);
            return true;
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
