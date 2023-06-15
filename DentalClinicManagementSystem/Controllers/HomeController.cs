using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentalClinicManagementSystem.Models;
using DentalClinicManagementSystem.Utils;

namespace DentalClinicManagementSystem.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Meet Our Team";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information";

            return View();
        }

        public ActionResult SendEmail()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string[] toEmails = model.ToEmail.Split(',');
                    string subject = model.Subject;
                    string contents = model.Contents;
                    var attachFile = postedFile;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    BinaryReader b = new BinaryReader(postedFile.InputStream);
                    byte[] binData = b.ReadBytes(postedFile.ContentLength);
                    string file = Convert.ToBase64String(binData);

                    EmailSender es = new EmailSender();

                    for (int idx = 0; idx < toEmails.Length; idx++)
                    {
                        if (attachFile != null)
                        {
                            es.Send(toEmails[idx].Trim(), subject, contents, fileName, file);
                        }
                        else
                        {
                            es.Send(toEmails[idx].Trim(), subject, contents);
                        }
                    }

                    Stream a = postedFile.InputStream;
                    ViewBag.Result = "Email(s) sent.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Dental Services";

            return View();
        }
    }
}