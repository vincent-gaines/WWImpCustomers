using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWImpCustomers.Infrastructure;
using WWImpCustomers.Services;
using WWImpCustomers.Data;

namespace WWImpCustomers.Startup
{
    public static class DependencyInjection
    {
        public static ServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<ICustomerRepository>(sp =>
                new CustomerRepository("Server=YOURSERVER;Database=WideWorldImporters;Integrated Security=true;",
                                       sp.GetRequiredService<ILogger>()));
            services.AddSingleton<ICustomerService, CustomerService>();

            return services.BuildServiceProvider();
        }
    }
}
