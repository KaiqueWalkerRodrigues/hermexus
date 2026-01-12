using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUsername(string username);
    }
}
