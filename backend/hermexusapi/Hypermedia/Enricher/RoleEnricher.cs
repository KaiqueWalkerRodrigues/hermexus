using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;

namespace hermexusapi.Hypermedia.Enricher
{
    public class RoleEnricher : ContentResponseEnricher<RoleDTO>
    {
        protected override Task EnrichModel(
            RoleDTO content, IUrlHelper urlHelper
            )
        {
            var requet = urlHelper.ActionContext.HttpContext.Request;
            var baseUrl = $"{requet.Scheme}://{requet.Host.ToUriComponent()}{requet.PathBase.ToUriComponent()}/api/role/v1";
            content.Links.AddRange(GenerateLinks(content.Id, baseUrl));
            return Task.CompletedTask;
        }

        private IEnumerable<HypermediaLink> GenerateLinks(long id, string baseUrl)
        {
            //return new List<HypermediaLink>
            return [
            
                // This "new HypermediaLink" is equal to new()
                new()
                {
                    Rel = RelationType.COLLECTION,
                    Href = $"{baseUrl}/asc/10/1",
                    Type = ResponseTypeFormat.DefaultGet,
                    Action = HttpActionVerb.GET
                },
                new()
                {
                    Rel = RelationType.SELF,
                    Href = $"{baseUrl}/{id}",
                    Type = ResponseTypeFormat.DefaultGet,
                    Action = HttpActionVerb.GET
                },
                new()
                {
                    Rel = RelationType.CREATE,
                    Href = $"{baseUrl}",
                    Type = ResponseTypeFormat.DefaultPost,
                    Action = HttpActionVerb.POST
                },
                new()
                {
                    Rel = RelationType.UPDATE,
                    Href = $"{baseUrl}",
                    Type = ResponseTypeFormat.DefaultPut,
                    Action = HttpActionVerb.PUT
                },
                new()
                {
                    Rel = RelationType.DELETE,
                    Href = $"{baseUrl}/{id}",
                    Type = ResponseTypeFormat.DefaultDelete,
                    Action = HttpActionVerb.DELETE
                },
            ];
        }
    }
}
