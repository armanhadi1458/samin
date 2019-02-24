namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news_add_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "ShowDashboard", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "ShowDashboard");
        }
    }
}
