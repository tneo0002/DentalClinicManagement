namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DatePicker_To_Appointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointmentDatetime");
            DropColumn("dbo.Appointments", "AppointmentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentStatus", c => c.String());
            AddColumn("dbo.Appointments", "AppointmentDatetime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointmentDate");
            DropColumn("dbo.Appointments", "PatientId");
        }
    }
}
