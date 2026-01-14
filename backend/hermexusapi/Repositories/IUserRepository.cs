using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUsername(string username);
        User Disable(long id);
        PagedSearch<User> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );


    }
}
