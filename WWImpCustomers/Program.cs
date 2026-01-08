using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WWImpCustomers.Data;
using WWImpCustomers.Services;
using WWImpCustomers.Startup;

namespace WWImpCustomers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DependencyInjection.Configure();  // initializes the static ServiceProvider

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form1(
                DependencyInjection.ServiceProvider.GetRequiredService<ICustomerService>(),
                DependencyInjection.ServiceProvider.GetRequiredService<ILookupRepository>()
            );

            Application.Run(form);
        }

    }
}
