namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedboolpropertytoFeedbacksDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "IsRead");
        }
    }
}
