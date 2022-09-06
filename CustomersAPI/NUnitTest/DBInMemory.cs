using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Data.Context;
using CustomerAPI.Domain;

namespace CustomerAPITest
{
    public class DBInMemory
    {
        DataContext _context;
        public DBInMemory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)                
                .EnableSensitiveDataLogging()
                .Options;
            _context = new DataContext(options);
            InsertFakeData();
        }

        public DataContext GetContext() => _context;
        private void InsertFakeData()
        {
            if(_context.Database.EnsureCreated())
            {                
                _context.Customers.Add(new Customer(1, "teste1", "teste1@example.com"));
                _context.Customers.Add(new Customer(2, "teste2", "teste2@example.com"));                
                _context.Customers.Add(new Customer(3, "teste3", "teste3@example.com"));                
                _context.Customers.Add(new Customer(4, "teste4", "teste4@example.com"));                
                _context.Customers.Add(new Customer(5, "teste5", "teste5@example.com"));                
                _context.Customers.Add(new Customer(6, "teste6", "teste6@example.com"));                
                _context.Customers.Add(new Customer(7, "teste7", "teste7@example.com"));                
                _context.Customers.Add(new Customer(8, "teste8", "teste8@example.com"));                
                _context.Customers.Add(new Customer(9, "teste9", "teste9@example.com"));                
                _context.Customers.Add(new Customer(10, "teste10", "teste10@example.com"));                
                _context.Customers.Add(new Customer(11, "teste11", "teste11@example.com"));                
                _context.Customers.Add(new Customer(12, "teste12", "teste12@example.com"));                
                _context.Customers.Add(new Customer(13, "teste13", "teste13@example.com"));                
                _context.Customers.Add(new Customer(14, "teste14", "teste14@example.com"));                
                _context.Customers.Add(new Customer(15, "teste15", "teste15@example.com"));                

                _context.SaveChanges();
            }
        }
    }
}
