using CustomerAPI.Domain;
using CustomerAPI.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moq;
using CustomerAPI.Data.Context;
using CustomerAPI.Service.Service;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Data.Repositories;
using System.Threading.Tasks;
using CustomerAPITest;

namespace NUnitTest.Services
{

    public class CustomerServiceTests
    {                
        private CustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            var dbInMemory = new DBInMemory();                        
            _customerService = new CustomerService(new CustomerRepository(dbInMemory.GetContext()));

        }

        #region Repository
        [Test]
        public async Task GetAll()
        {           
            Assert.AreEqual(10, _customerService.getCustomers(0, 10).Result.Count());
        }
        [Test]
        public async Task GetByEmail()
        {           
            Assert.AreEqual("teste1@example.com", _customerService.getCustomerByEmail("teste1@example.com").Result.Email);
        }   

        [Test]
        public async Task Delete()
        {            
            var customer = new Customer(1, "teste1", "teste1@example.com");
            await _customerService.deleteCustomer(customer);
            var result = _customerService.getCustomerByEmail("teste1@example.com").Result;
            Assert.IsNull(result);
        }

        [Test]
        public async Task Add()
        {
            Customer customer = new Customer { Email = "teste16@example.com", Name = "teste16" };
            Customer customerResult = _customerService.addCustomer(customer).Result;
            Customer customerValidate = _customerService.getCustomerByEmail(customer.Email).Result;
            Assert.AreEqual(customerValidate.Id, customerResult.Id);
        }

        [Test]
        public async Task Update()
        {            
            Customer customer = _customerService.getCustomerByEmail("teste1@example.com").Result;
            customer.Email = "teste1Update@example.com";
            Customer customerResult = _customerService.updateCustomer("teste1@example.com", customer).Result;
            Customer customerValidate = _customerService.getCustomerByEmail(customer.Email).Result;
            Assert.AreEqual(customer.Email, customerResult.Email);
        }
        #endregion        
    }
}