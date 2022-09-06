using CustomerAPI.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomerAPI.Data.Repositories;
using System.Threading.Tasks;
using CustomerAPITest;

namespace NUnitTest.Repositories
{

    public class CustomerRepositoryTests
    {        
        private CustomerRepository _customerRepository;

        [SetUp]
        public void Setup()
        {
            var dbInMemory = new DBInMemory();
            _customerRepository = new CustomerRepository(dbInMemory.GetContext());

        }

        #region Repository
        [Test]
        public async Task GetAll()
        {           
            Assert.AreEqual(10, _customerRepository.GetAll(0, 10).Result.Count());
        }
        [Test]
        public async Task GetByEmail()
        {           
            Assert.AreEqual("teste1@example.com", _customerRepository.GetByEmail("teste1@example.com").Result.Email);
        }

        [Test]
        public async Task Delete()
        {            
            var customer = new Customer(1, "teste1", "teste1@example.com");
            await _customerRepository.Delete(customer);
            var result = _customerRepository.GetByEmail("teste1@example.com").Result;
            Assert.IsNull(result);
        }

        [Test]
        public async Task Add()
        {
            Customer customer = new Customer { Email = "teste16@example.com", Name = "teste16" };
            Customer customerResult = _customerRepository.Add(customer).Result;
            Customer customerValidate = _customerRepository.GetByEmail(customer.Email).Result;
            Assert.AreEqual(customerValidate.Id, customerResult.Id);
        }

        [Test]
        public async Task Update()
        {            
            Customer customer = _customerRepository.GetByEmail("teste1@example.com").Result;
            customer.Email = "teste1Update@example.com";
            Customer customerResult = _customerRepository.Update(customer).Result;
            Customer customerValidate = _customerRepository.GetByEmail(customer.Email).Result;
            Assert.AreEqual(customer.Email, customerResult.Email);
        }

        #endregion       
    }
}