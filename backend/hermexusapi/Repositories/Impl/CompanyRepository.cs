using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;

namespace hermexusapi.Repositories.Impl
{
    public class CompanyRepository(MySQLContext context)
        : GenericRepository<Company>(context), ICompanyRepository
    {
        public Company Disable(long id)
        {
            var company = _context.Companies.Find(id);
            if (company == null) return null;
            company.Is_active = false;
            _context.SaveChanges();
            return company;
        }

        public List<Company> FindByName(string name)
        {
            var query = _context.Companies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return [.. query];
        }

        public PagedSearch<Company> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new CompanyQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _companys = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<Company>
            {
                Current_page = page,
                List = _companys,
                Page_size = size,
                Sort_directions = sort,
                Total_results = _totalResults
            };
        }
    }
}
