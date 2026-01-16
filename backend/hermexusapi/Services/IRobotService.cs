using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface IRobotService
    {
        RobotDTO Create(RobotDTO robot);
        RobotDTO FindById(long id);
        RobotDTO Update(RobotDTO robot);
        bool Delete(long id);
        PagedSearchDTO<RobotDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
    }
}
