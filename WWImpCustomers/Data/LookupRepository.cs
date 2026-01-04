using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; // Use only Microsoft.Data.SqlClient to resolve ambiguity
using Microsoft.Extensions.Logging;
using WWImpCustomers.Data;
using WWImpCustomers.Models;

namespace WideWorldImportersCustomers.Data
{
    public class LookupRepository : ILookupRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public LookupRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerCategory>> GetCustomerCategoriesAsync()
        {
            var list = new List<CustomerCategory>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(
                    @"SELECT CustomerCategoryID, CustomerCategoryName
                      FROM Sales.CustomerCategories
                      ORDER BY CustomerCategoryName", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new CustomerCategory
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            var list = new List<Person>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(
                    @"SELECT PersonID, FullName
                      FROM Application.People
                      ORDER BY FullName", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Person
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var list = new List<City>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(
                    @"SELECT CityID, CityName
                      FROM Application.Cities
                      ORDER BY CityName", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new City
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var list = new List<DeliveryMethod>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(
                    @"SELECT DeliveryMethodID, DeliveryMethodName
                      FROM Application.DeliveryMethods
                      ORDER BY DeliveryMethodName", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new DeliveryMethod
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return list;
        }

    }
}