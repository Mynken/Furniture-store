namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurWorkPhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileWorkDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        WorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => t.WorkId);
            
            AlterColumn("dbo.Works", "Description", c => c.String(nullable: false, maxLength: 1500));
            DropColumn("dbo.Works", "ImageData");
            DropColumn("dbo.Works", "ImageMimeType");
            DropColumn("dbo.Works", "ImageData1");
            DropColumn("dbo.Works", "ImageMimeType1");
            DropColumn("dbo.Works", "ImageData2");
            DropColumn("dbo.Works", "ImageMimeType2");
            DropColumn("dbo.Works", "ImageData3");
            DropColumn("dbo.Works", "ImageMimeType3");
            DropColumn("dbo.Works", "ImageData4");
            DropColumn("dbo.Works", "ImageMimeType4");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Works", "ImageMimeType4", c => c.String());
            AddColumn("dbo.Works", "ImageData4", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType3", c => c.String());
            AddColumn("dbo.Works", "ImageData3", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType2", c => c.String());
            AddColumn("dbo.Works", "ImageData2", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType1", c => c.String());
            AddColumn("dbo.Works", "ImageData1", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType", c => c.String());
            AddColumn("dbo.Works", "ImageData", c => c.Binary());
            DropForeignKey("dbo.FileWorkDetails", "WorkId", "dbo.Works");
            DropIndex("dbo.FileWorkDetails", new[] { "WorkId" });
            AlterColumn("dbo.Works", "Description", c => c.String(nullable: false));
            DropTable("dbo.FileWorkDetails");
        }
    }
}
