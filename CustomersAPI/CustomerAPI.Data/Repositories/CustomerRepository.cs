using CustomerAPI.Data.Context;
using CustomerAPI.Domain.Interfaces;
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
        public async Task<List<Customer>> GetAll(int skip, int take)
        {
            List<Customer> customers = await _context
               .Customers
               .AsNoTracking()
               .Skip(skip)
               .Take(take)
               .ToListAsync();
            return customers;
        }
                
        public async Task<Customer> GetByEmail(string email)
        {            
            Customer customer = _context.Customers
                .AsNoTracking()
                .Where(item => item.Email == email)
                .FirstOrDefault();            
            return  customer;
        }  
        public async Task<Customer> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task<Customer> Update( Customer customer)
        {
            _context.ChangeTracker.Clear();
            _context.Entry<Customer>(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }
        
        public async Task Delete(Customer customer)
        {
            _context.ChangeTracker.Clear();
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();         
        }


    }
}
