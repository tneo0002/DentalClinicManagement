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
using DentalClinicManagementSystem.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalClinicManagementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private AppointmentDentist db = new AppointmentDentist();

        // GET: Appointments
        [Authorize(Roles="Admin,Dentist,Patient")]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();

            if (User.IsInRole("Admin"))
            {
                var appointments = db.Appointments.Include(a => a.Dentist).OrderBy(a => a.AppointmentDate).ToList();
                return View(appointments);
            }
            else if (User.IsInRole("Dentist"))
            {
                var appointments = db.Appointments.Where(a => a.Dentist.UserId == UserId).Include(a => a.Dentist).OrderBy(a => a.AppointmentDate).ToList();
                return View(appointments);
            }
            else
            {
                var appointments = db.Appointments.Where(a => a.UserId == UserId).Include(a => a.Dentist).OrderBy(a => a.AppointmentDate).ToList();
                return View(appointments);
            }
            
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Patient")]
        public ActionResult Create()
        {
            ViewBag.DentistId = new SelectList(db.Dentists, "DentistId", "DentistName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "AppointmentId,DentistId,AppointmentDate,AppointmentFname,AppointmentLname,AppointmentPhone")] Appointment appointment)
        {
            
            try
            {
                ModelState.Clear();
                if (db.Appointments.Where(a => a.AppointmentDate == appointment.AppointmentDate && a.DentistId == appointment.DentistId).Any())
                {
                    ModelState.AddModelError("AppointmentDate", "Time slot already occupied");
                }
                string id = User.Identity.GetUserId();
                appointment.UserId = User.Identity.GetUserId();
                appointment.AppointmentCreatedOn = DateTime.Now.Date;
                appointment.AppointmentEmail = User.Identity.GetUserName();

                var patient = db.Patients.Where(p => p.UserId == id).FirstOrDefault();

                if (patient != null)
                {
                    appointment.AppointmentFname = patient.PatientFname;
                    appointment.AppointmentLname = patient.PatientLname;
                    appointment.AppointmentPhone = patient.PatientPhone;
                    appointment.AppointmentEmail = patient.PatientEmail;

                    
                    TryValidateModel(appointment);

                    if (ModelState.IsValid)
                    {
                        db.Appointments.Add(appointment);
                        db.SaveChanges();

                        try
                        {
                            String toEmail = User.Identity.GetUserName();
                            String subject = "Appointment Confirmation";
                            String contents = "Your appointment on " + appointment.AppointmentDate + " has been successfully made.";
                            String toPhone = "+61" + appointment.AppointmentPhone.Substring(1);

                            EmailSender es = new EmailSender();
                            es.Send(toEmail, subject, contents);
                            
                            SmsSender ss = new SmsSender();
                            ss.Send(toPhone, contents);

                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            return (View("Invalid mobile number. Please provide a valid mobile number to receive SMS notificaiton."));
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Create", "Patients");
                }

            }
            catch (Exception)
            {
                return(View("Error"));
            }
            
            ViewBag.DentistId = new SelectList(db.Dentists, "DentistId", "DentistName", appointment.DentistId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "DentistId", "DentistName", appointment.DentistId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "AppointmentId,DentistId,AppointmentDate,AppointmentFname,AppointmentLname,AppointmentPhone")] Appointment appointment)
        {
            appointment.UserId = User.Identity.GetUserId();

            appointment.AppointmentCreatedOn = DateTime.Now.Date;

            ModelState.Clear();
            TryValidateModel(appointment); 
            
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();


                try
                {
                    String toEmail = User.Identity.GetUserName();
                    String subject = "Change of Appointment";
                    String contents = "Your appointment has been successfully changed to " + appointment.AppointmentDate + ".";
                    String toPhone = "+61" + appointment.AppointmentPhone.Substring(1);

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    SmsSender ss = new SmsSender();
                    ss.Send(toPhone, contents);

                    return RedirectToAction("Index");
                }
                catch (Exception )
                {
                    return (View("Invalid mobile number. Please provide a valid mobile number to receive SMS notificaiton."));
                }
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "DentistId", "DentistName", appointment.DentistId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
