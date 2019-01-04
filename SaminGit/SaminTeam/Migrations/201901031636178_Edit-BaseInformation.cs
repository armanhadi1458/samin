namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditBaseInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseInformations", "FaceBook", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaseInformations", "FaceBook");
        }
    }
}
