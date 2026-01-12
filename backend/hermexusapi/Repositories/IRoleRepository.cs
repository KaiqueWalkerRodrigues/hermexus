using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Disable(long id);
        List<Role> FindByName(string name);
        PagedSearch<Role> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
