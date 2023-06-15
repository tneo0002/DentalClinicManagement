using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentalClinicManagementSystem.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public string AdminFname { get; set; }

        public string AdminLname { get; set; }

        public string UserId { get; set; }
    }
}