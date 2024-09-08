using LAF.Models.EFClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LAF
{
    namespace MySQLDatastore
    {
        public static class SeedData
        {
            public static void EnsurePopulated(IApplicationBuilder app)
            {
                EnsurePopulated(app.ApplicationServices.GetRequiredService<MySQLDbContext>());
            }

            public static void EnsurePopulated(MySQLDbContext context)
            {
                Console.WriteLine("Applying Migrations...");

                context.Database.Migrate();
                if (!context.Agents.Any())
                {
                    Console.WriteLine("Creating Seed Data...");

                    context.Agents.AddRange(new Agent("345653", "John Smith", 8, 9, 7, 8, 10),
                        new Agent("654378", "Jane Doe", 7, 8, 8, 7, 20),
                        new Agent("S875645", "Bob Johnson", 9, 10, 8, 9, 30),
                        new Agent("349875", "Mike Williams", 6, 7, 7, 6, 20),
                        new Agent("2387564", "Emma Johnson", 10, 9, 10, 10, 10),
                        new Agent("5437653", "Judy Jetson", 4, 5, 6, 7, 8)
                    );

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Seed Data Not Required...");
                }
            }
        }
    }
}
