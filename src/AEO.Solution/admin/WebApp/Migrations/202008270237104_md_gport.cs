namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_gport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GPorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 8),
                        cn_name = c.String(maxLength: 128),
                        en_name = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GPorts");
        }
    }
}
