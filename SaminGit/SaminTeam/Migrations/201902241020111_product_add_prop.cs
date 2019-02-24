namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_add_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShowDashboard", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShowDashboard");
        }
    }
}
