using LAF.Middleware;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders;
using LAF.Services.Middleware;

namespace LAF
{
    namespace MVC
    {
        public class Startup(IWebHostEnvironment env)
        {
            private readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .Build();

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<IConfiguration>(Configuration);

                //TODO: Add config setting to specify provider.
                services.AddSingleton<IAgentDataProvider, MySQLDataProvider>();
                services.AddScoped<IAgentMatchLogProvider, AgentMatchLogProvider>();

                services.AddMvc();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (!env.IsDevelopment())
                {
                    app.UseHsts();
                    app.UseExceptionHandler("/Home/Error");
                }
                else
                {
                    app.UseDeveloperExceptionPage();
                }

                // Must be declared before Authentication.
                //TODO: Was meant to be an extension method, but I can't get it to work.
                app.UseMiddleware<RequestAgentMatchMiddleware>();

                app.UseStaticFiles();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseRouting();

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    //Forward client IP and originating scheme (HTTP/HTTPS). This is to support HTTPS redirects.
                    // ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }
        }
    }
}
