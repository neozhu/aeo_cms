namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvName = c.String(maxLength: 80),
                        InvType = c.String(nullable: false, maxLength: 100),
                        InvCountry = c.String(maxLength: 100),
                        InvTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxNo = c.String(maxLength: 100),
                        InvUse = c.String(maxLength: 256),
                        Remark = c.String(maxLength: 256),
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
            DropForeignKey("dbo.CustomerInvoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerInvoices", new[] { "CustomerId" });
            DropTable("dbo.CustomerInvoices");
        }
    }
}
