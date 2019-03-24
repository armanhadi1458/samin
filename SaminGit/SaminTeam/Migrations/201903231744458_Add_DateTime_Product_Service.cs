namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DateTime_Product_Service : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Services", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Date");
            DropColumn("dbo.Products", "Date");
        }
    }
}
