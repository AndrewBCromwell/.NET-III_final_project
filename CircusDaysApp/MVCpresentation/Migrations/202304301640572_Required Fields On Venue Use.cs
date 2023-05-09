namespace MVCpresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFieldsOnVenueUse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VenueUses", "EmployeeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VenueUses", "EmployeeId");
        }
    }
}
