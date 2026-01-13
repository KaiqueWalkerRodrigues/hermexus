using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;
using System;

namespace hermexusapi.Repositories.Impl
{
    public class RoleRepository(MySQLContext context)
        : GenericRepository<Role>(context), IRoleRepository
    {
        public List<Role> FindByName(string name)
        {
            var query = _context.Roles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return [.. query];
        }

        public PagedSearch<Role> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new RoleQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _roles = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<Role>
            {
                CurrentPage = page,
                List = _roles,
                PageSize = size,
                SortDirections = sort,
                TotalResults = _totalResults
            };
        }
    }
}
