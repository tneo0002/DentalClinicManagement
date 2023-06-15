using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DentalClinicManagementSystem.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Email address")]
        [RegularExpression(@"\s*(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*", ErrorMessage = "Invalid Email Address(es). For multiple receivers, separate addresses by commas")]
        [Required(ErrorMessage = "Email address(es) required.")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter the subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }
    }
}