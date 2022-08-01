using CustomerAPI.Domain;

namespace CustomerAPI.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetByEmail(string email);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(string email,Customer customer);
        Task<Customer> Delete(Customer customer);        
    }
}
