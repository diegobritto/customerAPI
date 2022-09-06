namespace CustomerAPI.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> getCustomers(int skip, int take);
        Task<Customer> getCustomerByEmail(string email);
        Task<Customer> addCustomer(Customer customer);
        Task<Customer> updateCustomer(string email, Customer customer);
        Task deleteCustomer(Customer customer);        
    }
}
