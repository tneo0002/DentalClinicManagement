using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using DentalClinicManagementSystem.Models;

namespace DentalClinicManagementSystem.Context
{
    public class AppointmentDentist : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}