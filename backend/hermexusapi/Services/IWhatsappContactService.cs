using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;

namespace hermexusapi.Services
{
    public interface IWhatsappContactService
    {
        WhatsappContactDTO Create(WhatsappContactDTO role);
        WhatsappContactDTO FindById(long id);
        WhatsappContactDTO Update(WhatsappContactDTO role);
        bool Delete(long id);
        PagedSearchDTO<WhatsappContactDTO> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page);
    }
}
