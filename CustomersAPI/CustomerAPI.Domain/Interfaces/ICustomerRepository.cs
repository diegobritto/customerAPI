namespace CustomerAPI.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll(int skip, int take);
        Task<Customer> GetByEmail(string email);        
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task Delete(Customer customer);        
    }
}
