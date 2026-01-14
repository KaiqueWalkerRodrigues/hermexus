using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace hermexusapi.Repositories.Impl
{
    public class UserRepository(MySQLContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        public override User Update(User item)
        {
            var existingUser = _context.Users.Find(item.Id);
            if (existingUser == null) return null;

            _context.Entry(existingUser).CurrentValues.SetValues(item);

            _context.Entry(existingUser).Property(u => u.Password).IsModified = false;

            _context.SaveChanges();
            return existingUser;
        }

        public User Disable(long id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return null;
            user.Is_active = false;
            _context.SaveChanges();
            return user;
        }

        public User FindByUsername(string username)
        {
            return _context.Users.SingleOrDefault(
                u => u.Username == username);
        }

        public PagedSearch<User> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new UserQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _users = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<User>
            {
                CurrentPage = page,
                List = _users,
                PageSize = size,
                SortDirections = sort,
                TotalResults = _totalResults
            };
        }
        PagedSearch<User> IUserRepository.FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new UserQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _users = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<User>
            {
                CurrentPage = page,
                List = _users,
                PageSize = size,
                SortDirections = sort,
                TotalResults = _totalResults
            };
        }
    }
}
