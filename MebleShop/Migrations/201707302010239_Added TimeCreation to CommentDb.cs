namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeCreationtoCommentDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coments", "TimeCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coments", "TimeCreated");
        }
    }
}
