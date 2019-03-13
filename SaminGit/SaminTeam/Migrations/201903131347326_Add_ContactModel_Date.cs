namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ContactModel_Date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Date");
        }
    }
}
