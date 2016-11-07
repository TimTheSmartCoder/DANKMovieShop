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
    public class GenresController : ApiController
    {
        private IManager<Genre> _manager = new ManagerFacade().GetGenreManager();

        // GET: api/Genres
        public IEnumerable<Genre> GetGenres()
        {
            return _manager.ReadAll();
        }

        // GET: api/Genres/5
        [ResponseType(typeof(Genre))]
        [HttpGet]
        public IHttpActionResult GetGenre(int id)
        {
            Genre genre = _manager.ReadOne(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Id)
            {
                return BadRequest();
            }

            _manager.Update(genre);
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Genres
        [ResponseType(typeof(Genre))]
        [HttpPost]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manager.Create(genre);
            
            return CreatedAtRoute("DefaultApi", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(Genre))]
        [HttpDelete]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = _manager.ReadOne(id);
            if (genre == null)
            {
                return NotFound();
            }

            _manager.Delete(genre.Id);
            
            return Ok(genre);
        }
        
    }
}