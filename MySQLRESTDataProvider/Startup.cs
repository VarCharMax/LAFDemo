using LAF.BusinessLogic.ExtensionMethods;
using LAF.MySQLDatastore;
using LAF.Models.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace LAF
{
    namespace MySQLRESTDataProvider
    {
        public class Startup(IWebHostEnvironment env)
        {
            private readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddEnvironmentVariables()
                    .Build();

            public void ConfigureServices(IServiceCollection services)
            {
                string connectionstring = Configuration["ConnectionStrings:AgentConnection"].ToString();

                //Update connection string with passed in values if any
                Dictionary<string, string> dictConn = connectionstring.CSToDict();

                dictConn["host"] = Configuration["DBHOST"] ?? dictConn["host"];
                dictConn["port"] = Configuration["DBPORT"] ?? dictConn["port"];
                dictConn["password"] = Configuration["DBPASSWORD"] ?? dictConn["password"];

                connectionstring = $"{dictConn.DictToString()};";

                services.AddDbContext<MySQLDbContext>(options =>
                    options.UseMySQL(connectionstring));

                services.AddSingleton<IConfiguration>(Configuration);
                services.AddTransient<IAgentRepository, AgentMySQLRepository>();
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
                    SeedData.EnsurePopulated(app);
                }

                app.UseStaticFiles();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseRouting();

                // Must be declared before Authentication.

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
