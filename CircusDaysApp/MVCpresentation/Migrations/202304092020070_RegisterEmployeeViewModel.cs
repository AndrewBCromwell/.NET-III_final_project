namespace MVCpresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterEmployeeViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        GivenName = c.String(),
                        FamilyName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.RegisterEmployeeViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        GivenName = c.String(nullable: false, maxLength: 50),
                        FamilyName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterEmployeeViewModels");
            DropTable("dbo.Employees");
        }
    }
}
