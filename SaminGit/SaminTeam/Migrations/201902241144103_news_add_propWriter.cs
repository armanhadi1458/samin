namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news_add_propWriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Writer", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Writer");
        }
    }
}
