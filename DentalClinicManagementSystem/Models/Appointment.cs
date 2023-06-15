using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.ComponentModel;

namespace DentalClinicManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        [Display(Name = "Dentist")]
        public int DentistId { get; set; }

        public virtual Dentist Dentist { get; set; }

        [Display(Name = "Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Given Name")]
        public string AppointmentFname { get; set; }

        [Display(Name = "Surname")]
        public string AppointmentLname { get; set; }

        [Display(Name = "Booking Made On")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentCreatedOn { get; set; }

        [Display(Name = "Mobile Phone")]
        public string AppointmentPhone { get; set; }

        [Display(Name = "Email")]
        public string AppointmentEmail { get; set; }

        public string UserId { get; set; }
    }
}