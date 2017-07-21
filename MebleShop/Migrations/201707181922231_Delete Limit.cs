namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLimit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Works", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "Description", c => c.String(nullable: false, maxLength: 1500));
        }
    }
}
