using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;

namespace hermexusapi.Services.Impl
{
    public class PermissionServiceImpl(
        IPermissionRepository repository
            ) : IPermissionService
    {
        private IPermissionRepository _repository = repository;

        public PermissionDTO Create(PermissionDTO permission)
        {
            var entity = permission.Adapt<Permission>();
            entity = _repository.Create(entity);
            return entity.Adapt<PermissionDTO>();
        }

        public PermissionDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<PermissionDTO>();
        }

        public PermissionDTO Update(PermissionDTO permission)
        {
            var entity = permission.Adapt<Permission>();
            entity = _repository.Update(entity);
            return entity.Adapt<PermissionDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<PermissionDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<PermissionDTO>>();
        }
    }
}
