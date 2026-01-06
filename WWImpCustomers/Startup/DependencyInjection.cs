using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideWorldImportersCustomers.Data;
using WWImpCustomers.Data;
using WWImpCustomers.Infrastructure;
using WWImpCustomers.Services;

namespace WWImpCustomers.Startup
{
    public static class DependencyInjection
    {
        private static string _conn = "Server=DESKTOP-1GGPEFA;Database=WideWorldImporters;Integrated Security=true;";

        public static ServiceProvider ServiceProvider { get; private set; }

        public static void Configure()
        {
            var services = new ServiceCollection();

            // Register your services, repositories, loggers, etc.
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<ICustomerRepository>(sp =>
                new CustomerRepository(_conn, (Microsoft.Extensions.Logging.ILogger)sp.GetRequiredService<ILogger>()));
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ILookupRepository>(sp =>
                new LookupRepository(_conn, (Microsoft.Extensions.Logging.ILogger)sp.GetRequiredService<ILogger>()));

            // Build the provider and store it in the static property
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
