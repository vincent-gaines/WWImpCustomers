using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWImpCustomers.Data;
using WWImpCustomers.Models;

namespace WWImpCustomers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly ILogger _logger;

        public CustomerService(ICustomerRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public Task<IEnumerable<Customer>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Customer> GetAsync(int id) => _repo.GetByIdAsync(id);
        public Task<int> CreateAsync(Customer c) => _repo.AddAsync(c);
        public Task<bool> UpdateAsync(Customer c) => _repo.UpdateAsync(c);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    
      public Task<IEnumerable<Customer>> SearchAsync(string text)
        {
            return _repo.SearchAsync(text);
        }
    }

}
