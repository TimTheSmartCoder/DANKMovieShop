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
    public class MoviesController : ApiController
    {
        private IManager<Movie> _manager = new ManagerFacade().GetMovieManager();
        

        // GET: api/Movies
        public IEnumerable<Movie> GetMovies()
        {
            return _manager.ReadAll();
        }

        // GET: api/Movies/5
        [ResponseType(typeof(Movie))]
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _manager.ReadOne(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            _manager.Update(movie);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        [HttpPost]
        public IHttpActionResult PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manager.Create(movie);
            
            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = _manager.ReadOne(id);
            if (movie == null)
            {
                return NotFound();
            }

            _manager.Delete(movie.Id);
           
            return Ok(movie);
        }
        
    }
}