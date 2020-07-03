namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_customerupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerCommunications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CommType = c.String(nullable: false, maxLength: 20),
                        Status = c.String(maxLength: 20),
                        Salesman = c.String(maxLength: 20),
                        RefUsers = c.String(maxLength: 128),
                        BeginDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Remark = c.String(maxLength: 20),
                        CustomerCode = c.String(nullable: false, maxLength: 20),
                        CustomerName = c.String(nullable: false, maxLength: 80),
                        CustomerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerCommunications", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerCommunications", new[] { "CustomerId" });
            DropTable("dbo.CustomerCommunications");
        }
    }
}
