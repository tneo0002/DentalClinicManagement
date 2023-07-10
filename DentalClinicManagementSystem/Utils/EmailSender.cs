using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DentalClinicManagementSystem.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "API_KEY";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("4dm1n.ear2ear@gmail.com", "Ear2Ear Dentistry");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void Send(String toEmail, String subject, String contents, String fileName, String file)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("4dm1n.ear2ear@gmail.com", "Ear2Ear Dentistry");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            msg.AddAttachment(fileName, file);
            var response = client.SendEmailAsync(msg);
        }
    }
}