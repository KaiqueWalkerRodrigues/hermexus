using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface IPermissionService
    {
        PermissionDTO Create(PermissionDTO permission);
        PermissionDTO FindById(long id);
        PermissionDTO Update(PermissionDTO permission);
        bool Delete(long id);
        PagedSearchDTO<PermissionDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
    }
}
