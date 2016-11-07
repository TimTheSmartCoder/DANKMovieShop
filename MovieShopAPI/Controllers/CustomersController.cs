using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Entities;
using MovieShopBackend;
using MovieShopBackend.Contexts;

namespace MovieShopAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private IManager<Customer> _manager = new ManagerFacade().GetCustomerManager();
        

        // GET: api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _manager.ReadAll();
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _manager.ReadOne(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _manager.Update(customer);
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manager.Create(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _manager.ReadOne(id);
            if (customer == null)
            {
                return NotFound();
            }

            _manager.Delete(customer.Id);

            return Ok(customer);
        }

        
    }
}