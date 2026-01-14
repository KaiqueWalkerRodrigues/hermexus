using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;

namespace hermexusapi.Services.Impl
{
    public class SectorServiceImpl(
        ISectorRepository repository
            ) : ISectorService 
    {
        private ISectorRepository _repository = repository;
        public SectorDTO Create(SectorDTO sector)
        {
            var entity = sector.Adapt<Sector>();
            entity = _repository.Create(entity);
            return entity.Adapt<SectorDTO>();
        }

        public SectorDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<SectorDTO>();
        }

        public SectorDTO Update(SectorDTO sector)
        {
            var entity = sector.Adapt<Sector>();
            entity = _repository.Update(entity);
            return entity.Adapt<SectorDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<SectorDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<SectorDTO>>();
        }

        public SectorDTO Disable(long id)
        {
            var entity = _repository.Disable(id);
            return entity.Adapt<SectorDTO>();
        }
    }
}
