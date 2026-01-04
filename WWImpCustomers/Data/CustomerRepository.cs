using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WWImpCustomers.Models;



namespace WWImpCustomers.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _conn;
        private readonly ILogger _logger;

        public CustomerRepository(string conn, ILogger logger)
        {
            _conn = conn;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var list = new List<Customer>();

            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(
                @"SELECT CustomerID, CustomerName, PhoneNumber, FaxNumber, WebsiteURL,
                     CustomerCategoryID, PrimaryContactPersonID, DeliveryMethodID, DeliveryCityID
              FROM Sales.Customers", conn))
            {
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Customer
                        {
                            CustomerID = reader.GetInt32(0),
                            CustomerName = reader.GetString(1),
                            PhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                            FaxNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                            WebsiteURL = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CustomerCategoryID = reader.GetInt32(5),
                            PrimaryContactPersonID = reader.GetInt32(6),
                            DeliveryMethodID = reader.GetInt32(7),
                            DeliveryCityID = reader.GetInt32(8)
                        });
                    }
                }
            }

            return list;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(
                @"SELECT CustomerID, CustomerName, PhoneNumber, FaxNumber, WebsiteURL,
                 CustomerCategoryID, PrimaryContactPersonID, DeliveryMethodID, DeliveryCityID
          FROM Sales.Customers
          WHERE CustomerID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null;

                    return new Customer
                    {
                        CustomerID = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        PhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                        FaxNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                        WebsiteURL = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CustomerCategoryID = reader.GetInt32(5),
                        PrimaryContactPersonID = reader.GetInt32(6),
                        DeliveryMethodID = reader.GetInt32(7),
                        DeliveryCityID = reader.GetInt32(8)
                    };
                }
            }
        }
            
        public async Task<int> AddAsync(Customer c)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(
                @"INSERT INTO Sales.Customers
              (CustomerName, PhoneNumber, FaxNumber, WebsiteURL,
               CustomerCategoryID, PrimaryContactPersonID, DeliveryMethodID, DeliveryCityID,
               BillToCustomerID, PostalCityID, AccountOpenedDate, StandardDiscountPercentage,
               IsStatementSent, IsOnCreditHold)
              VALUES
              (@Name, @Phone, @Fax, @URL,
               @Cat, @Person, @Method, @City,
               @BillTo, @Postal, GETDATE(), 0, 0, 0);
              SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@Name", c.CustomerName);
                cmd.Parameters.AddWithValue("@Phone", (object)c.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", (object)c.FaxNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", (object)c.WebsiteURL ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat", c.CustomerCategoryID);
                cmd.Parameters.AddWithValue("@Person", c.PrimaryContactPersonID);
                cmd.Parameters.AddWithValue("@Method", c.DeliveryMethodID);
                cmd.Parameters.AddWithValue("@City", c.DeliveryCityID);

                // Simplified defaults
                cmd.Parameters.AddWithValue("@BillTo", 1);
                cmd.Parameters.AddWithValue("@Postal", 1);

                await conn.OpenAsync();
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<bool> UpdateAsync(Customer c)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(
                @"UPDATE Sales.Customers
              SET CustomerName = @Name,
                  PhoneNumber = @Phone,
                  FaxNumber = @Fax,
                  WebsiteURL = @URL,
                  CustomerCategoryID = @Cat,
                  PrimaryContactPersonID = @Person,
                  DeliveryMethodID = @Method,
                  DeliveryCityID = @City
              WHERE CustomerID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", c.CustomerID);
                cmd.Parameters.AddWithValue("@Name", c.CustomerName);
                cmd.Parameters.AddWithValue("@Phone", (object)c.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", (object)c.FaxNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", (object)c.WebsiteURL ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat", c.CustomerCategoryID);
                cmd.Parameters.AddWithValue("@Person", c.PrimaryContactPersonID);
                cmd.Parameters.AddWithValue("@Method", c.DeliveryMethodID);
                cmd.Parameters.AddWithValue("@City", c.DeliveryCityID);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(
                @"DELETE FROM Sales.Customers WHERE CustomerID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}
