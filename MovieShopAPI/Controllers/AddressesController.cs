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
    public class AddressesController : ApiController
    {
        private IManager<Address> _manager = new ManagerFacade().GetAddressManager();
        

        // GET: api/Addresses
        public IEnumerable<Address> GetAddresses()
        {
            return _manager.ReadAll();
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        [HttpGet]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = _manager.ReadOne(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            _manager.Update(address);
            

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manager.Create(address);
            
            return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = _manager.ReadOne(id);
            if (address == null)
            {
                return NotFound();
            }

            _manager.Delete(address.Id);

            return Ok(address);
        }

        
    }
}