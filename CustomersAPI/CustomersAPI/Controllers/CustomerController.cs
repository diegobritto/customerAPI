using CustomerAPI.Data.Context;
using CustomerAPI.Data.Interfaces;
using CustomerAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Util;
namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        
        public CustomerController(ICustomerService service)
        {            
            _service = service;
        }
        
        #region Method

        [HttpGet]
        [Route("getCustomers")]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var result = await _service.getCustomers();            
            return Ok(result);
        }

        [HttpGet]
        [Route("getCustomerByEmail/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            if(!Util.IsValidEmail(email))
                return BadRequest(new { message = Message.Text(Message.INVALID_EMAIL) });
            
            var result = await _service.getCustomerByEmail(email);                                    
            return Ok(result);
        }

        [HttpPost]
        [Route("addCustomer")]
        public async Task<ActionResult<Customer>> AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Customer result = await _service.addCustomer(customer);
                if (result == null)
                    return BadRequest(new { message = Message.Text(Message.REGISTERED_CUSTOMER_ERROR) });

                return Ok(new { message = Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS) });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = Message.Text(Message.REGISTERED_CUSTOMER_CONCURRENCY_ERROR) });
            }
            catch (Exception)
            {
                return BadRequest(new { message = Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS) });
            }
        }

        [HttpPut]
        [Route("updateCustomer/{email}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string email, [FromBody] Customer customer)
        {
            if (Util.IsValidEmail(email))
                return BadRequest(new { message = Message.Text(Message.INVALID_EMAIL) });
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);                        
            try
            {
                Customer result = await _service.updateCustomer(email, customer);
                if (result == null)
                    return NotFound(new { message = Message.Text(Message.CUSTOMER_NOT_FOUND) });

                return Ok(new { message = Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS) });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = Message.Text(Message.REGISTERED_CUSTOMER_CONCURRENCY_ERROR) });
            }
            catch (Exception)
            {
                return BadRequest(new { message = Message.Text(Message.REGISTERED_CUSTOMER_SUCCESS) });
            }
        }

        [HttpDelete]
        [Route("deleteCustomer/{email}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string email)
        {
            try
            {
                Customer customer = _service.getCustomerByEmail(email).Result;
                if (customer == null)
                    return NotFound(new { message = Message.Text(Message.CUSTOMER_NOT_FOUND) });
             
                await _service.deleteCustomer(customer);
                return Ok(new {message = Message.Text(Message.DELETED_CUSTOMER_SUCCESS) });
            }
            catch (Exception)
            {
                return BadRequest(new { message = Message.Text(Message.DELETED_CUSTOMER_ERROR) });
            }            
        }

        #endregion
    }
}
