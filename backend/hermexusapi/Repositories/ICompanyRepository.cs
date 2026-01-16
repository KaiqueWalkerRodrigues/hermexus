using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company Disable(long id);
        PagedSearch<Company> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
