using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWImpCustomers.Models;

namespace WWImpCustomers.Data
{
    public interface ILookupRepository
    {
        Task<IEnumerable<CustomerCategory>> GetCustomerCategoriesAsync();
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
