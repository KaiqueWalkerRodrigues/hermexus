using hermexusapi.Models;
using hermexusapi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace hermexusapi.Repositories.Impl
{
    public class UserRepository(MySQLContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        public User FindByUsername(string username)
        {
            return _context.Users.SingleOrDefault(
                u => u.Username == username);
        }
    }
}
