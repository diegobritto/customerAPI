using CustomerAPI.Domain;

namespace CustomerAPI.Data.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> getCustomers();
        Task<Customer> getCustomerByEmail(string email);
        Task<Customer> addCustomer(Customer customer);
        Task<Customer> updateCustomer(string email,Customer customer);
        Task<Customer> deleteCustomer(Customer customer);        
    }
}
