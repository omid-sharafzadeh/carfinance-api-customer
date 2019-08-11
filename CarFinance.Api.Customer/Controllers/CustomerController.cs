using System;
using System.Net;
using System.Threading.Tasks;
using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarFinance.Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var customer = await _customerService.GetAll();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var customer = await _customerService.GetById(id);

                if (customer == null) return NotFound(id);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var createdCustomer = await _customerService.Add(customer);
                return Created("", createdCustomer);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Customer updatedCustomer)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return BadRequest(ModelState);

                var customerToBeUpdated = _customerService.GetById(updatedCustomer.Id);

                if (customerToBeUpdated == null) return NotFound();
                
                await _customerService.Update(updatedCustomer);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var customerToBeDeleted = await _customerService.GetById(id);

                if (customerToBeDeleted == null) return NotFound();
                
                await _customerService.Delete(customerToBeDeleted);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
