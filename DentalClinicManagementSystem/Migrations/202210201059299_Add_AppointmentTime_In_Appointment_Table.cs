namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AppointmentTime_In_Appointment_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "AppointmentTime");
        }
    }
}
