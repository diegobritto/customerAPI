using CustomerAPI.Data.Context;
using CustomerAPI.Domain.Interfaces;
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
        
        /// <summary>
        /// endpoint, com paginação, responsavel por buscar todos os clientes cadastrados no banco de dados.
        /// </summary>
        /// <param name="skip">permitirá que você ignore os primeiros n itens</param>
        /// <param name="take">obterá um número n de itens da fonte de dados</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCustomers")]
        public async Task<ActionResult<List<Customer>>> GetCustomers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = await _service.getCustomers(skip,take);            
            return Ok(result);
        }
        
        /// <summary>
        /// endpoint responsavel por buscar um cliente com determinado email no banco de dados
        /// </summary>
        /// <param name="email">email do cliente a ser buscado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCustomerByEmail/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            if(!Util.IsValidEmail(email))
                return BadRequest(new { message = Message.Text(Message.INVALID_EMAIL) });
            
            var result = await _service.getCustomerByEmail(email);                                    
            return Ok(result);
        }
        
        /// <summary>
        /// endpoint responsavel por adicionar um novo cliente no banco de dados
        /// </summary>
        /// <param name="customer">novo registro de cliente</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// endpoint responsavel por atualizar um cliente, com determinado email, no banco de dados
        /// </summary>
        /// <param name="email">email do cliente a ser atualizado</param>
        /// <param name="customer">dados atualizados do cliente</param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateCustomer/{email}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string email, [FromBody] Customer customer)
        {
                        
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
        
        /// <summary>
        /// endpoint responsavel por deletar um registro do tipo Cliente do banco de dados
        /// </summary>
        /// <param name="email">email do cliente a ser deletado</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteCustomer/{email}")]
        public async Task<ActionResult> DeleteCustomer(string email)
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
