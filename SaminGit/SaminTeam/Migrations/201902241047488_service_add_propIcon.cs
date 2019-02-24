namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service_add_propIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Icon", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Icon");
        }
    }
}
