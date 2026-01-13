using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;

namespace hermexusapi.Repositories.Impl
{
    public class SectorRepository(MySQLContext context)
        : GenericRepository<Sector>(context), ISectorRepository
    {
        public Sector Disable(long id)
        {
            var sector = _context.Sectors.Find(id);
            if (sector == null) return null;
            sector.Is_active = false;
            _context.SaveChanges();
            return sector;
        }

        public List<Sector> FindByName(string name)
        {
            var query = _context.Sectors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return [.. query];
        }

        public PagedSearch<Sector> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new SectorQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _sectors = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<Sector>
            {
                CurrentPage = page,
                List = _sectors,
                PageSize = size,
                SortDirections = sort,
                TotalResults = _totalResults
            };
        }
    }
}
