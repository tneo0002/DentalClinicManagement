using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DentalClinicManagementSystem.Context;
using DentalClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace DentalClinicManagementSystem.Controllers
{
    public class DentistsController : Controller
    {
        private AppointmentDentist db = new AppointmentDentist();

        // GET: Dentists
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();

            if (User.IsInRole("Admin"))
            {
                var dentists = db.Dentists.OrderBy(d => d.DentistName).ToList();
                return View(dentists);
            }
            else if (User.IsInRole("Dentist"))
            {
                var dentists = db.Dentists.Where(d => d.UserId == UserId).OrderBy(d => d.DentistName).ToList();
                return View(dentists);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Dentists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentist dentist = db.Dentists.Find(id);
            if (dentist == null)
            {
                return HttpNotFound();
            }
            return View(dentist);
        }

        // GET: Dentists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dentists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "DentistId,DentistName")] Dentist dentist)
        {
            dentist.UserId = User.Identity.GetUserId();

            ModelState.Clear();
            TryValidateModel(dentist);

            if (ModelState.IsValid)
            {
                db.Dentists.Add(dentist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dentist);
        }

        // GET: Dentists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentist dentist = db.Dentists.Find(id);
            if (dentist == null)
            {
                return HttpNotFound();
            }
            return View(dentist);
        }

        // POST: Dentists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DentistId,DentistName")] Dentist dentist)
        {
            dentist.UserId = User.Identity.GetUserId();

            ModelState.Clear();
            TryValidateModel(dentist); 
            
            if (ModelState.IsValid)
            {
                db.Entry(dentist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dentist);
        }

        // GET: Dentists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dentist dentist = db.Dentists.Find(id);
            if (dentist == null)
            {
                return HttpNotFound();
            }
            return View(dentist);
        }

        // POST: Dentists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dentist dentist = db.Dentists.Find(id);
            db.Dentists.Remove(dentist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
