namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PageInformation_Uniq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PageInformations", "UniqueTitle", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageInformations", "UniqueTitle", c => c.String());
        }
    }
}
