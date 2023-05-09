namespace MVCpresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityModelsforVenueUseandVenueUseVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VenueUses",
                c => new
                    {
                        VenueUseId = c.Int(nullable: false, identity: true),
                        VenueId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AdCampain = c.Int(),
                        TotalTicketsSold = c.Int(nullable: false),
                        TotalRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VenueUseVMId = c.Int(),
                        VenueName = c.String(),
                        StreetAddress = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.VenueUseId);
            
            CreateTable(
                "dbo.UseDays",
                c => new
                    {
                        UseDayId = c.Int(nullable: false, identity: true),
                        UseId = c.Int(nullable: false),
                        UseDate = c.DateTime(nullable: false),
                        TicketsSold = c.Int(nullable: false),
                        Revenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                        VenueUseVM_VenueUseId = c.Int(),
                    })
                .PrimaryKey(t => t.UseDayId)
                .ForeignKey("dbo.VenueUses", t => t.VenueUseVM_VenueUseId)
                .Index(t => t.VenueUseVM_VenueUseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UseDays", "VenueUseVM_VenueUseId", "dbo.VenueUses");
            DropIndex("dbo.UseDays", new[] { "VenueUseVM_VenueUseId" });
            DropTable("dbo.UseDays");
            DropTable("dbo.VenueUses");
        }
    }
}
