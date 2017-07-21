namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        WorkId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.WorkId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Works");
        }
    }
}
