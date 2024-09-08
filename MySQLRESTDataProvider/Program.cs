using LAF.MySQLRESTDataProvider;
using LAF.MySQLDatastore;

var config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

//In order to support this commandline option, we are using the older Startup class approach. which MS says is not deprecated.

if ((config["INITDB"] ?? "false") == "true")
{
    Console.WriteLine("Preparing database ...");
    SeedData.EnsurePopulated(new MySQLDbContext());
    Console.WriteLine("Database preparation complete");
}
else
{
    Console.WriteLine("Starting ASP.NET...");
    var host = new WebHostBuilder()
    .UseConfiguration(config)
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseStartup<Startup>()
    .Build();

    host.Run();
}
