namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service_add_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "ShowDashboard", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "ShowDashboard");
        }
    }
}
