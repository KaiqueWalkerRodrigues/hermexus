using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface ISectorService
    {
        SectorDTO Create(SectorDTO sector);
        SectorDTO FindById(long id);
        List<SectorDTO> FindAll();
        SectorDTO Update(SectorDTO sector);
        bool Delete(long id);
        List<SectorDTO> FindByName(string name);
        PagedSearchDTO<SectorDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
        SectorDTO Disable(long id);
    }
}
