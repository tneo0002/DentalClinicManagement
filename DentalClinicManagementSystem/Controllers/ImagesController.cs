using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DentalClinicManagementSystem.Context;
using DentalClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace DentalClinicManagementSystem.Controllers
{
    public class ImagesController : Controller
    {
        private AppointmentDentist db = new AppointmentDentist();

        // GET: Images
        [Authorize(Roles = "Admin,Dentist,Patient")]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();

            if (User.IsInRole("Admin") || User.IsInRole("Dentist"))
            {
                var images = db.Images.OrderBy(a => a.ImageName).ToList();
                return View(images);
            }
            else
            {
                var images = db.Images.Where(a => a.UserId == UserId).OrderBy(a => a.ImageName).ToList();
                return View(images);
            }            
        }

        // GET: Images/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Image image = db.Images.Find(id);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(image);
        //}

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,ImageName")] Image image, HttpPostedFileBase postedFile)
        {
            image.UserId = User.Identity.GetUserId();

            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            image.ImagePath = myUniqueFileName;
            TryValidateModel(image);
            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = image.ImagePath + fileExtension;
                image.ImagePath = filePath;
                postedFile.SaveAs(serverPath + image.ImagePath);
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Image image = db.Images.Find(id);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(image);
        //}

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ImageId,ImagePath,ImageName,UserId")] Image image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(image).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(image);
        //}

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
