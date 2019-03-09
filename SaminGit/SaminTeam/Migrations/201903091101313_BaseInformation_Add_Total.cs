namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseInformation_Add_Total : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseInformations", "TotalProject", c => c.String(maxLength: 300));
            AddColumn("dbo.BaseInformations", "TotalEmployees", c => c.String(maxLength: 300));
            AddColumn("dbo.BaseInformations", "TotalClient", c => c.String(maxLength: 300));
            AddColumn("dbo.BaseInformations", "TotalAgency", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaseInformations", "TotalAgency");
            DropColumn("dbo.BaseInformations", "TotalClient");
            DropColumn("dbo.BaseInformations", "TotalEmployees");
            DropColumn("dbo.BaseInformations", "TotalProject");
        }
    }
}
