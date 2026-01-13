using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface ISectorRepository : IRepository<Sector>
    {
        Sector Disable(long id);
        List<Sector> FindByName(string name);
        PagedSearch<Sector> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
