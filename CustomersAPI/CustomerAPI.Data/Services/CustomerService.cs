using CustomerAPI.Data.Interfaces;
using CustomerAPI.Data.Repositories;
using CustomerAPI.Domain;

namespace CustomerAPI.Data.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<Customer> addCustomer(Customer customer)
        {
            Customer result = await getCustomerByEmail(customer.Email);
            if (result != null)
                return null;
            return await _repository.Add(customer);
        }

        public async Task<Customer> deleteCustomer(Customer customer)
        {
            return await _repository.Delete(customer);
        }

        public async Task<Customer> getCustomerByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public async  Task<List<Customer>> getCustomers()
        {
            return await _repository.GetAll();
        }

        public async Task<Customer> updateCustomer(int id, Customer customer)
        {
            Customer result = await _repository.GetById(id);            
            result.Email = customer.Email;
            result.Name = customer.Name;
            if (result == null)
                return null;
            return await _repository.Update(result);
        }
    }
}
