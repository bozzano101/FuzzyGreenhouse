using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GreenhouseCore.WebServer
{
    public class WebStartup
    {
        public void ConfigurationServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/ping", () => { return Results.Ok(); });
            });
        }
    }
}
