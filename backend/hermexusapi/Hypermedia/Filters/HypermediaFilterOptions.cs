using hermexusapi.Hypermedia.Abstract;

namespace hermexusapi.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ContentReponseEnricherList { get; set; } = [];
    }
}
