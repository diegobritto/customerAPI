using CustomerAPI.Domain;

namespace CustomerAPI.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetByEmail(string email);
        Task<Customer> GetById(int id);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<Customer> Delete(Customer customer);        
    }
}
