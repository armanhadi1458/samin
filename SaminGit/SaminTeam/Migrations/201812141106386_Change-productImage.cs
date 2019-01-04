namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeproductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductImages", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductImages", "Name");
        }
    }
}
