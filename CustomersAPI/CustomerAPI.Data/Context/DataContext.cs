using Microsoft.EntityFrameworkCore;
using CustomerAPI.Domain;
namespace CustomerAPI.Data.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
    }
}
