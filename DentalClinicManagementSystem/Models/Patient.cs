using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DentalClinicManagementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Your given name is required")]
        [Display(Name = "Given Name")]
        public string PatientFname { get; set; }

        [Required(ErrorMessage = "Your surname is required")]
        [Display(Name = "Surname")]
        public string PatientLname { get; set; }

        [Display(Name = "Address")]
        public string PatientAddress { get; set; }

        [Required(ErrorMessage = "A mobile number is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Enter a 10-digit mobile number.")]
        [Display(Name = "Mobile Phone")]
        public string PatientPhone { get; set; }

        [Display(Name = "Email")]
        public string PatientEmail { get; set; }
        
        public string UserId { get; set; }
    }
}