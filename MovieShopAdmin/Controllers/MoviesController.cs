using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MovieShopAdmin.Models.Movies;
using MovieShopBackend;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;

namespace MovieShopAdmin.Controllers
{
    public class MoviesController : Controller
    {
        private IManager<Movie> _manager = new ManagerFacade().GetMovieManager();
        private IManager<Genre> _genreManager = new ManagerFacade().GetGenreManager();

        // GET: Movies
        public ActionResult Index()
        {
            return View(this._manager.ReadAll());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = this._manager.ReadOne(id.GetValueOrDefault());

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            MoviesCreateViewModel moviesCreateViewModel = new MoviesCreateViewModel();

            moviesCreateViewModel.Genres = new SelectList(this._genreManager.ReadAll(), "Id", "Name");

            return View(moviesCreateViewModel);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Year,Price,ImageUrl,Trailer,GenreId")] MoviesCreateViewModel moviesCreateViewModel)
        {
            if (moviesCreateViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties.
                Movie movie = Mapper.Map<Movie>(moviesCreateViewModel);
                
                this._manager.Create(movie);

                return RedirectToAction("Index");
            }

            moviesCreateViewModel.Genres = new SelectList(this._genreManager.ReadAll(), "Id", "Name");

            return View(moviesCreateViewModel);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = this._manager.ReadOne(id.GetValueOrDefault());

            if (movie == null)
                return HttpNotFound();

            //Use AutoMapper to copy properties.
            MoviesEditViewModel moviesEditViewModel = Mapper.Map<MoviesEditViewModel>(movie);
            Mapper.Map(movie, moviesEditViewModel);

            moviesEditViewModel.Genres = new SelectList(this._genreManager.ReadAll(), "Id", "Name");

            return View(moviesEditViewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Price,ImageUrl,Trailer,GenreId")] MoviesEditViewModel moviesEditViewModel)
        {
            if (moviesEditViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties.
                Movie movie = Mapper.Map<Movie>(moviesEditViewModel);

                this._manager.Update(movie);

                return RedirectToAction("Index");
            }

            moviesEditViewModel.Genres = new SelectList(this._genreManager.ReadAll());
            
            return View(moviesEditViewModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = this._manager.ReadOne(id.GetValueOrDefault());

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this._manager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
