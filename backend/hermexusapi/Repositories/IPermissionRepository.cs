using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        List<Permission> FindByName(string name);
        PagedSearch<Permission> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
