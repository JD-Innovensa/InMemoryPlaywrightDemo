using System;
using System.Linq;
using AutomatedTesting.Factory;
using Employees.Data;
using Employees.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Employees.UITests.Common
{
    public static class Configuration
    {
        /// <summary>
        /// Name for overriding default "production" environment.
        /// </summary>
        public const string AspnetcoreEnvironmentAutomatedTesting = "AutomatedTesting";

        /// <summary>
        /// Set the override of web host configuration.
        /// </summary>
        public static Action<IWebHostBuilder> WebhostConfiguration => builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var employeeDbContextService = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<EmployeeDbContext>));

                    services.Remove(employeeDbContextService);

                    services.AddDbContext<EmployeeDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestEmployeeDbContext");
                    });


                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;

                        var logger = scopedServices.GetRequiredService<ILogger<AutomatedTestServerFactory<Startup>>>();

                        var employeeDbContext = scopedServices.GetRequiredService<EmployeeDbContext>();
                        employeeDbContext.Database.EnsureCreated();

                        try
                        {
                            // Seed database
                            InitializeDbForTests(employeeDbContext);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                        }
                    }
                });

            };

        /// <summary>
        /// Standard initial data for tests
        /// </summary>
        /// <param name="employeeDbContext"></param>
        public static void InitializeDbForTests(EmployeeDbContext employeeDbContext)
        {
            employeeDbContext.Employees.AddRange(
                new Employee { EmployeeId = 101, FirstName = "Adam", LastName = "Ant", JobTitle = "CEO Test", Telephone = "0101" },
                new Employee { EmployeeId = 102, FirstName = "Brenda", LastName = "Beetle", JobTitle = "CFO Test", Telephone = "0102" },
                new Employee { EmployeeId = 103, FirstName = "Colin", LastName = "Caterpillar", JobTitle = "CIO Test", Telephone = "0103" },
                new Employee { EmployeeId = 104, FirstName = "Diana", LastName = "Dragonfly", JobTitle = "COO Test", Telephone = "0104" }
                );

            employeeDbContext.SaveChanges();
        }
    }
}
