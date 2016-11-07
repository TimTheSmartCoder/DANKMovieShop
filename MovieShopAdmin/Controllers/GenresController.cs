using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MovieShopAdmin.Models.Genres;
using MovieShopBackend;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;

namespace MovieShopAdmin.Controllers
{
    public class GenresController : Controller
    {
       
        private IManager<Genre> _manager = new ManagerFacade().GetGenreManager();
        
        // GET: Genres
        public ActionResult Index()
        {
            return View(_manager.ReadAll());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Genre genre = _manager.ReadOne(id.GetValueOrDefault());

            if (genre == null)
                return HttpNotFound();

            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] GenresCreateViewModel genresCreateViewModel)
        {
            if (genresCreateViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties.
                Genre genre = Mapper.Map<Genre>(genresCreateViewModel);

                _manager.Create(genre);   
                             
                return RedirectToAction("Index");
            }

            return View(genresCreateViewModel);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               
            Genre genre = _manager.ReadOne(id.GetValueOrDefault());

            if (genre == null)
                return HttpNotFound();

            //Use Automapper to copy properties.
            GenresEditViewModel genresEditViewModel = Mapper.Map<GenresEditViewModel>(genre);
            Mapper.Map(genre, genresEditViewModel);

            return View(genresEditViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GenresEditViewModel genresEditViewModel)
        {
            if (genresEditViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties.
                Genre genre = Mapper.Map<Genre>(genresEditViewModel);

                _manager.Update(genre);

                return RedirectToAction("Index");
            }

            return View(genresEditViewModel);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               
            Genre genre = _manager.ReadOne(id.GetValueOrDefault());

            if (genre == null)
                return HttpNotFound();

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Genre genre = _manager.ReadOne(id.GetValueOrDefault());

            if (genre == null)
                return HttpNotFound();

            _manager.Delete(genre.Id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // _manager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
