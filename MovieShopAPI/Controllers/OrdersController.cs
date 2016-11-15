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
    public class OrdersController : ApiController
    {
        private IManager<Order> _manager = new ManagerFacade().GetOrderManager();
        

        // GET: api/Orders
        public IEnumerable<Order> GetOrders()
        {
            return _manager.ReadAll();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = _manager.ReadOne(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _manager.Update(order);
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manager.Create(order);

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = _manager.ReadOne(id);
            if (order == null)
            {
                return NotFound();
            }

            _manager.Delete(order.Id);

            return Ok(order);
        }

        
    }
}