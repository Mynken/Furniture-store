namespace MebleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedComentclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coments",
                c => new
                    {
                        ComentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Details = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ComentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Coments");
        }
    }
}
