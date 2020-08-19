namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_Inquiry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InquiryNo = c.String(maxLength: 20),
                        TaskNo = c.String(maxLength: 256),
                        Status = c.String(nullable: false, maxLength: 20),
                        Salesman = c.String(nullable: false, maxLength: 20),
                        BeginDate = c.DateTime(nullable: false),
                        FeedbackDate = c.DateTime(),
                        Demande = c.String(maxLength: 256),
                        CustomerId = c.Int(nullable: false),
                        CustomerCode = c.String(nullable: false, maxLength: 20),
                        CustomerName = c.String(nullable: false, maxLength: 80),
                        Country = c.String(maxLength: 50),
                        Cur = c.String(maxLength: 20),
                        ExchangeRate = c.Decimal(precision: 18, scale: 2),
                        ContactName = c.String(nullable: false, maxLength: 80),
                        ContactInfo = c.String(maxLength: 128),
                        EndDate = c.DateTime(nullable: false),
                        Urgency = c.String(maxLength: 20),
                        PreRemind = c.Int(nullable: false),
                        Check1 = c.Boolean(nullable: false),
                        Creator = c.String(maxLength: 20),
                        Executor = c.String(maxLength: 20),
                        Check2 = c.Boolean(nullable: false),
                        Check3 = c.Boolean(nullable: false),
                        Owner = c.String(maxLength: 20),
                        CompanyId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 128),
                        Ver = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.InquiryNo, unique: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.InquiryFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 100),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Folder = c.String(maxLength: 20),
                        FilePath = c.String(),
                        RelativePath = c.String(),
                        Owner = c.String(maxLength: 20),
                        Upload = c.DateTime(nullable: false),
                        Ext = c.String(maxLength: 100),
                        FileId = c.String(maxLength: 100),
                        Ver = c.Int(nullable: false),
                        InquiryId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId)
                .Index(t => t.InquiryId);
            
            CreateTable(
                "dbo.InquiryProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(maxLength: 50),
                        ProductName = c.String(maxLength: 200),
                        CategoryName = c.String(maxLength: 50),
                        ProductEnName = c.String(maxLength: 200),
                        CnDescription = c.String(),
                        EnDescription = c.String(),
                        ThirdProductNo = c.String(maxLength: 128),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(maxLength: 10),
                        Executor = c.String(maxLength: 20),
                        SupplierCode = c.String(maxLength: 20),
                        SupplierName = c.String(maxLength: 128),
                        SupplierProductNo = c.String(maxLength: 128),
                        PriceType = c.String(maxLength: 30),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Cur = c.String(maxLength: 20),
                        MinQty = c.Decimal(precision: 18, scale: 2),
                        PriceDate = c.DateTime(),
                        Feedback = c.String(),
                        Recommended = c.Boolean(nullable: false),
                        SamplePic = c.String(),
                        InquiryNo = c.String(nullable: false, maxLength: 20),
                        TaskNo = c.String(maxLength: 20),
                        Ver = c.Int(nullable: false),
                        InquiryId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
            CreateTable(
                "dbo.InquiryRefs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InquiryNo = c.String(nullable: false, maxLength: 20),
                        TaskNo = c.String(maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 20),
                        BeginDate = c.DateTime(nullable: false),
                        Salesman = c.String(maxLength: 20),
                        Dept = c.String(maxLength: 80),
                        Ver = c.Int(nullable: false),
                        InquiryId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .Index(t => t.InquiryId);
            
            CreateIndex("dbo.InquiryTasks", "TaskNo", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InquiryRefs", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.InquiryProducts", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.InquiryFiles", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.Inquiries", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Inquiries", "CompanyId", "dbo.Companies");
            DropIndex("dbo.InquiryTasks", new[] { "TaskNo" });
            DropIndex("dbo.InquiryRefs", new[] { "InquiryId" });
            DropIndex("dbo.InquiryProducts", new[] { "InquiryId" });
            DropIndex("dbo.InquiryFiles", new[] { "InquiryId" });
            DropIndex("dbo.Inquiries", new[] { "CompanyId" });
            DropIndex("dbo.Inquiries", new[] { "CustomerId" });
            DropIndex("dbo.Inquiries", new[] { "InquiryNo" });
            DropTable("dbo.InquiryRefs");
            DropTable("dbo.InquiryProducts");
            DropTable("dbo.InquiryFiles");
            DropTable("dbo.Inquiries");
        }
    }
}
