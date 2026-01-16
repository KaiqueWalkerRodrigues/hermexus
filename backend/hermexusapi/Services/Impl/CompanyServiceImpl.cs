using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;

namespace hermexusapi.Services.Impl
{
    public class CompanyServiceImpl(
        ICompanyRepository repository
            ) : ICompanyService
    {
        private ICompanyRepository _repository = repository;
        public CompanyDTO Create(CompanyDTO sector)
        {
            var entity = sector.Adapt<Company>();
            entity = _repository.Create(entity);
            return entity.Adapt<CompanyDTO>();
        }

        public CompanyDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<CompanyDTO>();
        }

        public CompanyDTO Update(CompanyDTO sector)
        {
            var entity = sector.Adapt<Company>();
            entity = _repository.Update(entity);
            return entity.Adapt<CompanyDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<CompanyDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<CompanyDTO>>();
        }

        public CompanyDTO Disable(long id)
        {
            var entity = _repository.Disable(id);
            return entity.Adapt<CompanyDTO>();
        }
    }
}
