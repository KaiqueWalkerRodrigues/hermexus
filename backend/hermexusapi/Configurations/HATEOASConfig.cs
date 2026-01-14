using hermexusapi.Hypermedia.Enricher;
using hermexusapi.Hypermedia.Filters;

namespace hermexusapi.Configurations
{
    public static class HATEOASConfig
    {
        public static IServiceCollection AddHATEOASConfiguration(
            this IServiceCollection services
            )
        {
            var filterOptions = new HypermediaFilterOptions();
            filterOptions.ContentReponseEnricherList.Add(
                new RoleEnricher()
                );
            filterOptions.ContentReponseEnricherList.Add(
                new SectorEnricher()
                );
            filterOptions.ContentReponseEnricherList.Add(
                new UserEnricher()
                );
            services.AddSingleton(filterOptions);
            services.AddScoped<HypermediaFilter>();
            return services;
        }

        public static void UseHATEOASRoutes(
            this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute(
                "Default", "{controller=values}/v1/{id?}"
                );
        }
    }
}
