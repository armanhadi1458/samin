namespace SaminProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PageInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageInformations",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        UniqueTitle = c.String(),
                        Title = c.String(nullable: false, maxLength: 200),
                        SubTitle = c.String(nullable: false, maxLength: 200),
                        FileName = c.String(),
                        OrginalFileName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PageInformations");
        }
    }
}
