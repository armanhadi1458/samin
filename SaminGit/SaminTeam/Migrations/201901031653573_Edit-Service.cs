namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditService : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Services", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "ID", c => c.Short(nullable: false, identity: true));
            AddPrimaryKey("dbo.Services", "ID");
        }
    }
}
