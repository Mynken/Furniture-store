namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedphototoWorkDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Works", "ImageData1", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType1", c => c.String());
            AddColumn("dbo.Works", "ImageData2", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType2", c => c.String());
            AddColumn("dbo.Works", "ImageData3", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType3", c => c.String());
            AddColumn("dbo.Works", "ImageData4", c => c.Binary());
            AddColumn("dbo.Works", "ImageMimeType4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Works", "ImageMimeType4");
            DropColumn("dbo.Works", "ImageData4");
            DropColumn("dbo.Works", "ImageMimeType3");
            DropColumn("dbo.Works", "ImageData3");
            DropColumn("dbo.Works", "ImageMimeType2");
            DropColumn("dbo.Works", "ImageData2");
            DropColumn("dbo.Works", "ImageMimeType1");
            DropColumn("dbo.Works", "ImageData1");
        }
    }
}
