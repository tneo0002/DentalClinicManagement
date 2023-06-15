namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminFname = c.String(),
                        AdminLname = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        DentistId = c.Int(nullable: false),
                        AppointmentDatetime = c.DateTime(nullable: false),
                        AppointmentFname = c.String(),
                        AppointmentLname = c.String(),
                        AppointmentCreatedOn = c.DateTime(nullable: false),
                        AppointmentStatus = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Dentists", t => t.DentistId, cascadeDelete: true)
                .Index(t => t.DentistId);
            
            CreateTable(
                "dbo.Dentists",
                c => new
                    {
                        DentistId = c.Int(nullable: false, identity: true),
                        DentistName = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.DentistId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        PatientFname = c.String(nullable: false),
                        PatientLname = c.String(nullable: false),
                        PatientAddress = c.String(),
                        PatientPhone = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "DentistId", "dbo.Dentists");
            DropIndex("dbo.Appointments", new[] { "DentistId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Dentists");
            DropTable("dbo.Appointments");
            DropTable("dbo.Admins");
        }
    }
}
