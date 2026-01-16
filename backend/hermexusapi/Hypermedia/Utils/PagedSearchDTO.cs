using hermexusapi.Hypermedia.Abstract;
using System.Xml.Serialization;

namespace hermexusapi.Hypermedia.Utils
{
    public class PagedSearchDTO<T> where T : ISupportsHypermedia
    {
        public int Current_page { get; set; }
        public int Page_size { get; set; }
        public int Total_results { get; set; }
        public string Sort_fields { get; set; }
        public string Sort_directions { get; set; } = "asc";

        [XmlIgnore]
        public Dictionary<string, object> Filters { get; set; } = [];

        public List<T> List { get; set; } = [];

        public PagedSearchDTO() { }

        public PagedSearchDTO(
            int currentPage,
            int pageSize,
            string sortFields,
            string sortDirections,
            Dictionary<string, object> filters)
        {
            Current_page = currentPage;
            Page_size = pageSize;
            Sort_fields = sortFields;
            Sort_directions = sortDirections;
            Filters = filters ?? [];
        }

        public PagedSearchDTO(
            int currentPage,
            string sortFields,
            string sortDirections
            )
            : this(currentPage, 10, sortFields, sortDirections, null) { }

        public int GetCurrentPage() => Current_page == 0 ? 1 : Current_page;
        public int GetPageSize() => Page_size == 0 ? 10 : Page_size;
    }
}