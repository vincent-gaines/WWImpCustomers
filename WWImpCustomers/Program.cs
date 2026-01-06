using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WWImpCustomers.Services;
using WWImpCustomers.Startup;

namespace WWImpCustomers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            DependencyInjection.Configure();
            var provider = DependencyInjection.ServiceProvider;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form1(provider.GetRequiredService<ICustomerService>());
            Application.Run(form);
        }
    }
}
