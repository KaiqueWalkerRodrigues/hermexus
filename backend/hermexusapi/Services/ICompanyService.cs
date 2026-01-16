using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface ICompanyService
    {
        CompanyDTO Create(CompanyDTO company);
        CompanyDTO FindById(long id);
        CompanyDTO Update(CompanyDTO company);
        bool Delete(long id);
        PagedSearchDTO<CompanyDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
        CompanyDTO Disable(long id);
    }
}
