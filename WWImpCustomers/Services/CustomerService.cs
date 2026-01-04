using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWImpCustomers.Models;

namespace WWImpCustomers.Services
{
    internal class CustomerService
    {
    }
    public Task<IEnumerable<Customer>> SearchAsync(string text)
        {
            return _repo.SearchAsync(text);
        }
    }
