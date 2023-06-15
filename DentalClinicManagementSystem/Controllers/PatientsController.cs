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
    public class PatientsController : Controller
    {
        private AppointmentDentist db = new AppointmentDentist();

        // GET: Patients
        [Authorize]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();

            if (User.IsInRole("Patient"))
            {
                var patients = db.Patients.Where(p => p.UserId == UserId).OrderBy(p => p.PatientLname).ToList();
                return View(patients);
            }
            else
            {
                var patients = db.Patients.OrderBy(p => p.PatientLname).ToList();
                return View(patients);
            }
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PatientId,PatientFname,PatientLname,PatientAddress,PatientPhone,PatientEmail")] Patient patient)
        {
            patient.PatientEmail = User.Identity.GetUserName();

            string id = User.Identity.GetUserId();

            var patientInDb = db.Patients.Where(p => p.UserId == id).FirstOrDefault();

            if (patientInDb == null)
            {
                patient.UserId = User.Identity.GetUserId();

                ModelState.Clear();
                TryValidateModel(patient);
                if (ModelState.IsValid)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction("Index", "Patients");
            }
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PatientId,PatientFname,PatientLname,PatientAddress,PatientPhone,PatientEmail")] Patient patient)
        {
            patient.UserId = User.Identity.GetUserId();

            patient.PatientEmail = User.Identity.GetUserName();

            ModelState.Clear();
            TryValidateModel(patient); 
            
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
