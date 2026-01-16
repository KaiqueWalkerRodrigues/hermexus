using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;

namespace hermexusapi.Services.Impl
{
    public class RobotServiceImpl(
        IRobotRepository repository
            ) : IRobotService
    {
        private IRobotRepository _repository = repository;

        public RobotDTO Create(RobotDTO robot)
        {
            var entity = robot.Adapt<Robot>();
            entity = _repository.Create(entity);
            return entity.Adapt<RobotDTO>();
        }

        public RobotDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<RobotDTO>();
        }

        public RobotDTO Update(RobotDTO robot)
        {
            var entity = robot.Adapt<Robot>();
            entity = _repository.Update(entity);
            return entity.Adapt<RobotDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<RobotDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<RobotDTO>>();
        }
    }
}
