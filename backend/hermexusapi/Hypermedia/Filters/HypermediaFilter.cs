using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hermexusapi.Hypermedia.Filters
{
    public class HypermediaFilter(
        HypermediaFilterOptions hypermediaFilterOptions)
        : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _hypermediaFilterOptions = hypermediaFilterOptions;
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enricher = _hypermediaFilterOptions
                    .ContentReponseEnricherList
                    .FirstOrDefault(e => e.CanEnrich(context));
                enricher?.Enrich(context).Wait();
            }
        }
    }
}
