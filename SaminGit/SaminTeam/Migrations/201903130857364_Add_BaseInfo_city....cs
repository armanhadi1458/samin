namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BaseInfo_city : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseInformations", "Country", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.BaseInformations", "City", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.BaseInformations", "Longtiude", c => c.String(maxLength: 25));
            AddColumn("dbo.BaseInformations", "Latiude", c => c.String(maxLength: 25));
            AddColumn("dbo.BaseInformations", "WorkTime", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaseInformations", "WorkTime");
            DropColumn("dbo.BaseInformations", "Latiude");
            DropColumn("dbo.BaseInformations", "Longtiude");
            DropColumn("dbo.BaseInformations", "City");
            DropColumn("dbo.BaseInformations", "Country");
        }
    }
}
