namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Project_ShowDashBoard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ShowDashboard", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "ShowDashboard");
        }
    }
}
