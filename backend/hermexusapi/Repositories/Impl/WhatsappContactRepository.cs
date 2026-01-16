using hermexusapi.Models;
using hermexusapi.Models.Context;
using hermexusapi.Repositories.QueryBuilder;

namespace hermexusapi.Repositories.Impl
{
    public class WhatsappContactRepository(MySQLContext context)
        : GenericRepository<WhatsappContact>(context), IWhatsappContactRepository
    {
        public List<WhatsappContact> FindByName(string name)
        {
            var query = _context.Whatsapp_contacts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name!.ToLower().Contains(name.ToLower()));
            return [.. query];
        }

        public PagedSearch<WhatsappContact> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new WhatsappContactQueryBuilder();
            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(
                name, sortDirection, pageSize, page);

            var _whatsappContact = base.FindWithPagedSearch(query);
            var _totalResults = base.GetCount(countQuery);

            return new PagedSearch<WhatsappContact>
            {
                Current_page = page,
                List = _whatsappContact,
                Page_size = size,
                Sort_directions = sort,
                Total_results = _totalResults
            };
        }
    }
}
