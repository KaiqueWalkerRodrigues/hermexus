using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface IRoleService
    {
        RoleDTO Create(RoleDTO role);
        RoleDTO FindById(long id);
        List<RoleDTO> FindAll();
        RoleDTO Update(RoleDTO role);
        bool Delete(long id);
        List<RoleDTO> FindByName(string name);
        PagedSearchDTO<RoleDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
    }
}
