using Scalar.AspNetCore;

namespace hermexusapi.Configurations
{
    public static class ScalarConfig
    {
        private static readonly string AppName = "Hermexusapi";

        public static WebApplication AddScalarConfiguration(
            this WebApplication app)
        {
            app.MapScalarApiReference("/scalar", options =>
            {
                options.WithTitle(AppName);
                options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);

            }).WithTags("scalar");

            return app;
        }
    }
}