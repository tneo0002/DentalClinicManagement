namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AppointmentEmail_And_PatientEmail_To_Respective_DBs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentPhone", c => c.String());
            AddColumn("dbo.Appointments", "AppointmentEmail", c => c.String());
            AddColumn("dbo.Patients", "PatientEmail", c => c.String());
            AlterColumn("dbo.Patients", "UserId", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentContact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentContact", c => c.String());
            AlterColumn("dbo.Patients", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.Patients", "PatientEmail");
            DropColumn("dbo.Appointments", "AppointmentEmail");
            DropColumn("dbo.Appointments", "AppointmentPhone");
        }
    }
}
