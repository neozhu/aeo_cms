namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_InquiryTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InquiryTaskProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(maxLength: 200),
                        CategoryName = c.String(maxLength: 128),
                        ProductEnName = c.String(maxLength: 200),
                        CnDescription = c.String(),
                        EnDescription = c.String(),
                        ThirdProductNo = c.String(maxLength: 128),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(maxLength: 10),
                        PriceType = c.String(maxLength: 30),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Executor = c.String(maxLength: 20),
                        SupplierCode = c.String(maxLength: 20),
                        SupplierName = c.String(maxLength: 128),
                        SamplePic = c.String(maxLength: 256),
                        TaskNo = c.String(maxLength: 20),
                        InquiryTaskId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InquiryTasks", t => t.InquiryTaskId, cascadeDelete: true)
                .Index(t => t.InquiryTaskId);
            
            CreateTable(
                "dbo.InquiryTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskNo = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 20),
                        Salesman = c.String(nullable: false, maxLength: 20),
                        CompanyId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 128),
                        CustomerId = c.Int(nullable: false),
                        CustomerCode = c.String(nullable: false, maxLength: 20),
                        CustomerName = c.String(nullable: false, maxLength: 80),
                        Country = c.String(maxLength: 50),
                        Cur = c.String(maxLength: 20),
                        ExchangeRate = c.Decimal(precision: 18, scale: 2),
                        ContactName = c.String(nullable: false, maxLength: 80),
                        ContactInfo = c.String(maxLength: 128),
                        BeginDate = c.DateTime(nullable: false),
                        Enddate = c.DateTime(nullable: false),
                        Urgency = c.String(maxLength: 20),
                        Demande = c.String(maxLength: 512),
                        PreRemind = c.Int(nullable: false),
                        Check1 = c.Boolean(nullable: false),
                        Creator = c.String(maxLength: 20),
                        Executor = c.String(maxLength: 20),
                        Check2 = c.Boolean(nullable: false),
                        Check3 = c.Boolean(nullable: false),
                        Owner = c.String(maxLength: 20),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InquiryTaskProducts", "InquiryTaskId", "dbo.InquiryTasks");
            DropForeignKey("dbo.InquiryTasks", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InquiryTasks", "CompanyId", "dbo.Companies");
            DropIndex("dbo.InquiryTasks", new[] { "CustomerId" });
            DropIndex("dbo.InquiryTasks", new[] { "CompanyId" });
            DropIndex("dbo.InquiryTaskProducts", new[] { "InquiryTaskId" });
            DropTable("dbo.InquiryTasks");
            DropTable("dbo.InquiryTaskProducts");
        }
    }
}
