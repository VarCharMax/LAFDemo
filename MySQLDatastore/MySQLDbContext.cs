using LAF.Models.EFClasses;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LAF
{
    namespace MySQLDatastore
    {
        public class MySQLDbContext : DbContext
        {
            public DbSet<Agent> Agents => Set<Agent>();

            public MySQLDbContext() { }

            public MySQLDbContext(DbContextOptions<MySQLDbContext> options) : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                var envs = Environment.GetEnvironmentVariables();

                var host = envs["DBHOST"] ?? "localhost";
                var port = envs["DBPORT"] ?? "3306";
                var password = envs["DBPASSWORD"] ?? "mysecret";

                var connectionString = $"server={host};port={port};database=products;user=root;password={password};";

                options.UseMySQL(connectionString);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
    }
}
