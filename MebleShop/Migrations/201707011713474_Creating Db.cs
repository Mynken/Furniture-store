namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Details = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FeedId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedbacks");
        }
    }
}
