namespace MVCpresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empoyeeID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "employee_id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "employee_id");
        }
    }
}
