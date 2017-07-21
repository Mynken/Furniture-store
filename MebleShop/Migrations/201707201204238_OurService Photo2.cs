namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurServicePhoto2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileServiceDetails", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.FileServiceDetails", new[] { "Service_ServiceId" });
            RenameColumn(table: "dbo.FileServiceDetails", name: "Service_ServiceId", newName: "ServiceId");
            AlterColumn("dbo.FileServiceDetails", "ServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.FileServiceDetails", "ServiceId");
            AddForeignKey("dbo.FileServiceDetails", "ServiceId", "dbo.Services", "ServiceId", cascadeDelete: true);
            DropColumn("dbo.FileServiceDetails", "WorkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileServiceDetails", "WorkId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FileServiceDetails", "ServiceId", "dbo.Services");
            DropIndex("dbo.FileServiceDetails", new[] { "ServiceId" });
            AlterColumn("dbo.FileServiceDetails", "ServiceId", c => c.Int());
            RenameColumn(table: "dbo.FileServiceDetails", name: "ServiceId", newName: "Service_ServiceId");
            CreateIndex("dbo.FileServiceDetails", "Service_ServiceId");
            AddForeignKey("dbo.FileServiceDetails", "Service_ServiceId", "dbo.Services", "ServiceId");
        }
    }
}
