using hermexusapi.Models;

namespace hermexusapi.Repositories
{
    public interface IWhatsappContactRepository : IRepository<WhatsappContact>
    {
        List<WhatsappContact> FindByName(string name);
        PagedSearch<WhatsappContact> FindWithPagedSearch(
            string name,
            string sortDirection,
            int pageSize,
            int page
            );
    }
}
