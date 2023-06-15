namespace DentalClinicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AppointmentContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentContact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "AppointmentContact");
        }
    }
}
