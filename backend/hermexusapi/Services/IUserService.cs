using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;

namespace hermexusapi.Services
{
    public interface IUserService
    {
        UserDTO Create(UserDTO user);
        UserDTO? FindById(long id);
        UserDTO? Update(UserDTO user);
        bool Delete(long id);
        PagedSearchDTO<UserDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
        UserDTO Disable(long id);
        User? FindByUsername(string username);
    }
}
