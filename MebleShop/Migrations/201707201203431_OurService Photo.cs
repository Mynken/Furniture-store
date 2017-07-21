namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurServicePhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileServiceDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        WorkId = c.Int(nullable: false),
                        Service_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.Service_ServiceId)
                .Index(t => t.Service_ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileServiceDetails", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.FileServiceDetails", new[] { "Service_ServiceId" });
            DropTable("dbo.Services");
            DropTable("dbo.FileServiceDetails");
        }
    }
}
