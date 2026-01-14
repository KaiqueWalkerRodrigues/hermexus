using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface ISectorRepository : IRepository<Sector>
    {
        Sector Disable(long id);
        PagedSearch<Sector> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
