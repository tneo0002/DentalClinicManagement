namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DentalClinicManagementSystem.Context.AppointmentDentist>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DentalClinicManagementSystem.Context.AppointmentDentist";
        }

        protected override void Seed(DentalClinicManagementSystem.Context.AppointmentDentist context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
