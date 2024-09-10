using LAF.BusinessLogic.ExtensionMethods;
using LAF.Models.Interfaces.Services;
using LAF.Services.Middleware;

//Using the new minimal config approach.

/*
 * Config
 */
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ToDo: Should this be done after injecting the service? Can you do this both ways?
// builder.Services.AddSingleton<IConfiguration>(config);

//Probably don't need both calls since they repeat config parsing.
builder.Services
    .AddDataProviderServiceConfig(builder.Configuration)
    .AddDataProviderServiceGroup(builder.Configuration);

builder.Services.AddScoped<IAgentMatchLogProvider, AgentMatchLogProvider>();

// builder.Services.AddAuthorization();
// builder.Services.AddControllers();


/*
 * Runtime environment.
 */
builder.WebHost
    .UseConfiguration(config)
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Custom middleware
app.UseAgentMatchService();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseStatusCodePages();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    //Forward client IP and originating scheme (HTTP/HTTPS). This is to support HTTPS redirects.
    // ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();