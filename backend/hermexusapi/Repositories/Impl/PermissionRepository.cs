using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;

namespace hermexusapi.Repositories.Impl
{
    public class PermissionRepository(MySQLContext context)
        : GenericRepository<Permission>(context), IPermissionRepository
    {
        public List<Permission> FindByName(string name)
        {
            var query = _context.Permissions.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return [.. query];
        }

        public PagedSearch<Permission> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new PermissionQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _permissions = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<Permission>
            {
                Current_page = page,
                List = _permissions,
                Page_size = size,
                Sort_directions = sort,
                Total_results = _totalResults
            };
        }
    }
}
