using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IRobotRepository : IRepository<Robot>
    {
        List<Robot> FindByName(string name);
        Robot Disable(long id);
        PagedSearch<Robot> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
