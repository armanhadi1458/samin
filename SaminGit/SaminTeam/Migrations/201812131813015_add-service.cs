namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addservice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Logo = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
        }
    }
}
