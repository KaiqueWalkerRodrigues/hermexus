using Mapster;
using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;

namespace hermexusapi.Services.Impl
{
    public class RoleServiceImpl(
        IRoleRepository repository
            ) : IRoleService
    {
        private IRoleRepository _repository = repository;

        public RoleDTO Create(RoleDTO role)
        {
            var entity = role.Adapt<Role>();
            entity = _repository.Create(entity);
            return entity.Adapt<RoleDTO>();
        }

        public RoleDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<RoleDTO>();
        }

        public RoleDTO Update(RoleDTO role)
        {
            var entity = role.Adapt<Role>();
            entity = _repository.Update(entity);
            return entity.Adapt<RoleDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<RoleDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<RoleDTO>>();
        }
    }
}
