using Microsoft.Data.SqlClient; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWImpCustomers
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var provider = DependencyInjection.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form1(provider.GetRequiredService<ICustomerService>());
            Application.Run(form);
        }
    }
}
