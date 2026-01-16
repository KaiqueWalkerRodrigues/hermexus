using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;

namespace hermexusapi.Repositories.Impl
{
    public class RobotRepository(MySQLContext context)
        : GenericRepository<Robot>(context), IRobotRepository
    {
        public Robot Disable(long id)
        {
            var robot = _context.Robots.Find(id);
            if (robot == null) return null;
            robot.Is_active = false;
            _context.SaveChanges();
            return robot;
        }

        public List<Robot> FindByName(string name)
        {
            var query = _context.Robots.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return query.ToList();
        }

        public PagedSearch<Robot> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new RobotQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _robots = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<Robot>
            {
                Current_page = page,
                List = _robots,
                Page_size = size,
                Sort_directions = sort,
                Total_results = _totalResults
            };
        }
    }
}