namespace hermexusapi.Models
{
    public class PagedSearch<T>
    {
        public int Current_page { get; set; }
        public int Page_size { get; set; }
        public string Sort_directions { get; set; } = "asc";
        public int Total_results { get; set; }
        public List<T>? List { get; set; }
    }
}
