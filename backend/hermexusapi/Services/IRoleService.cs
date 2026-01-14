using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface IRoleService
    {
        RoleDTO Create(RoleDTO role);
        RoleDTO FindById(long id);
        RoleDTO Update(RoleDTO role);
        bool Delete(long id);
        PagedSearchDTO<RoleDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
    }
}
