using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieShopAdmin.Models.Customers;
using MovieShopBackend;
using MovieShopBackend.Contexts;
using AutoMapper;
using Entities;
using ServiceGateway;

namespace MovieShopAdmin.Controllers
{
    public class CustomersController : Controller
    {
        private IServiceGateway<Customer> _manager = ServiceGatewayFactory.GetService<Customer>();
        private IServiceGateway<Address> _addressManager = ServiceGatewayFactory.GetService<Address>();
        
        // GET: Customers
        public ActionResult Index()
        {
            return View(_manager.ReadAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = _manager.ReadOne(id.GetValueOrDefault());

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,StreetName,StreetNumber,Country,ZipCode")] CustomersCreateViewModel postCustomersCreateViewModel)
        {
            if (postCustomersCreateViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties from ViewModel to Entities.
                Customer customer = Mapper.Map<Customer>(postCustomersCreateViewModel);
                Address address = Mapper.Map<Address>(postCustomersCreateViewModel);
                customer.Address = address;
                
                _manager.Create(customer);
                             
                return RedirectToAction("Index");
            }

            return View(postCustomersCreateViewModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Customer customer = _manager.ReadOne(id.GetValueOrDefault());

            if (customer == null)
                return HttpNotFound();

            //Map our information from entities to ViewModel.
            CustomerEditViewModel postCustomerEditViewModel = Mapper.Map<CustomerEditViewModel>(customer);
            postCustomerEditViewModel = Mapper.Map(customer.Address, postCustomerEditViewModel);

            return View(postCustomerEditViewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,StreetName,StreetNumber,Country,ZipCode")] CustomerEditViewModel postCustomerEditViewModel)
        {
            if (postCustomerEditViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties from ViewModel to Entities.
                Customer customer = Mapper.Map<Customer>(postCustomerEditViewModel);
                Address address = Mapper.Map<Address>(postCustomerEditViewModel);
                customer.Address = address;

                _manager.Update(customer);
                _addressManager.Update(address);
                              
                return RedirectToAction("Index");
            }
            return View(postCustomerEditViewModel);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = _manager.ReadOne(id.GetValueOrDefault());

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = _manager.ReadOne(id.GetValueOrDefault());

            if (customer == null)
                return HttpNotFound();

            _manager.Delete(customer.Id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
