using CustomerAPI.Data.Context;
using CustomerAPI.Data.Interfaces;
using CustomerAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext Context)
        {
            _context = Context;
        }
        public async Task<List<Customer>> GetAll()
        {
            List<Customer> customers = _context.Customers.AsNoTracking().ToList();
            return customers;
        }
                
        public async Task<Customer> GetByEmail(string email)
        {            
            Customer customer = _context.Customers.AsNoTracking().Where(item => item.Email == email).FirstOrDefault();            
            return customer;
        }
        public async Task<Customer> GetById(int id)
        {
            Customer customer = _context.Customers.AsNoTracking().Where(item => item.Id == id).FirstOrDefault();
            return customer;
        }
        public async Task<Customer> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task<Customer> Update( Customer customer)
        {            
            _context.Entry<Customer>(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task<Customer> Delete(Customer customer)
        {            
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }


    }
}
