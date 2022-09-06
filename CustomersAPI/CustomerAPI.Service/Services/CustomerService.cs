using CustomerAPI.Domain.Interfaces;
using CustomerAPI.Domain;

namespace CustomerAPI.Service.Service
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

        public async Task deleteCustomer(Customer customer)
        {
            await _repository.Delete(customer);
        }

        public async Task<Customer> getCustomerByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public async  Task<List<Customer>> getCustomers(int skip =0, int take=10)
        {
            return await _repository.GetAll(skip,take);
        }

        public async Task<Customer> updateCustomer(string email, Customer customer)
        {
            Customer result = await getCustomerByEmail(email);
            if (result == null)
                return null;
            return await _repository.Update(customer);
        }
    }
}
