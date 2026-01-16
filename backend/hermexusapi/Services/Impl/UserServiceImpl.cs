
using hermexusapi.Auth.Contract;
using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hermexusapi.Services.Impl
{
    public class UserServiceImpl(
        IUserRepository repository,
        IPasswordHasher passwordHasher
        ) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public UserDTO Create(UserDTO user)
        {
            var entity = new User
            {
                Username = user.Username,
                Password = _passwordHasher.Hash(user.Password),
                Name = user.Name,
                Is_active = user.Is_active,
                Refresh_token = string.Empty,
                Refresh_token_expiry_time = null
            };
            entity = _repository.Create(entity);
            entity.Password = "***********";
            entity.Refresh_token = "***********";
            entity.Refresh_token_expiry_time = null;
            return entity.Adapt<UserDTO>();
        }

        public UserDTO? FindById(long id)
        {
            var entity = _repository.FindById(id);

            if (entity == null)
            {
                return null;
            }

            entity.Password = "***********";
            entity.Refresh_token = "***********";
            entity.Refresh_token_expiry_time = null;

            return entity.Adapt<UserDTO>();
        }

        public UserDTO? Update(UserDTO user)
        {
            var entity = user.Adapt<User>();
            entity = _repository.Update(entity);
            if (entity == null)
            {
                return null;
            }
            entity.Password = "***********";
            entity.Refresh_token = "***********";
            entity.Refresh_token_expiry_time = null;
            return entity.Adapt<UserDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<UserDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);

            if (result?.List != null)
            {
                foreach (var user in result.List)
                {
                    if (user == null) continue;
                    user.Password = "***********";
                    user.Refresh_token = "***********";
                    user.Refresh_token_expiry_time = null;
                }
            }

            return result.Adapt<PagedSearchDTO<UserDTO>>();
        }

        public UserDTO Disable(long id)
        {
            var entity = _repository.Disable(id);
            return entity.Adapt<UserDTO>();
        }

        public User FindByUsername(string username)
        {
            return _repository.FindByUsername(username);
        }
    }
}
