namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baseInformationaddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseInformations", "Description", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaseInformations", "Description");
        }
    }
}
