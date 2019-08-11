using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarFinance.Api.Customer.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarFinance.Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDb _customerDb;

        public CustomerController(ICustomerDb customerDb)
        {
            _customerDb = customerDb;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var createdCustomer = await _customerDb.Insert(customer);
                return Created("", createdCustomer);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
