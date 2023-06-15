namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_AppointmentTime_Field : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "AppointmentTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentTime", c => c.DateTime(nullable: false));
        }
    }
}
