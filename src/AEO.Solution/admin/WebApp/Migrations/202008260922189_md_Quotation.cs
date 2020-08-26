namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_Quotation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuotationFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 100),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Folder = c.String(maxLength: 20),
                        FileId = c.String(maxLength: 100),
                        Ext = c.String(maxLength: 100),
                        FilePath = c.String(maxLength: 256),
                        RelativePath = c.String(maxLength: 256),
                        RefKey = c.String(maxLength: 100),
                        Owner = c.String(maxLength: 20),
                        Upload = c.DateTime(nullable: false),
                        QpNo = c.String(maxLength: 20),
                        QuotationId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .Index(t => t.QuotationId);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QpNo = c.String(maxLength: 20),
                        Salesman = c.String(nullable: false, maxLength: 20),
                        CompanyId = c.Int(nullable: false),
                        CompanyCode = c.String(nullable: false, maxLength: 20),
                        CompanyName = c.String(maxLength: 128),
                        CustomerId = c.Int(nullable: false),
                        CustomerCode = c.String(nullable: false, maxLength: 20),
                        CustomerName = c.String(nullable: false, maxLength: 80),
                        Country = c.String(maxLength: 50),
                        ContactName = c.String(nullable: false, maxLength: 80),
                        ContactInfo = c.String(maxLength: 128),
                        QuoteDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        LoadingPort = c.String(maxLength: 128),
                        DischargePort = c.String(maxLength: 128),
                        Cur = c.String(maxLength: 20),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceTerm = c.String(maxLength: 20),
                        PayMode = c.String(maxLength: 128),
                        GoodsAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormName = c.String(maxLength: 20),
                        Remark = c.String(maxLength: 256),
                        InquiryNo = c.String(maxLength: 20),
                        TaskNo = c.String(maxLength: 20),
                        Ver = c.Int(nullable: false),
                        Initiator = c.String(maxLength: 32),
                        SubmitDate = c.DateTime(),
                        ToAuditor = c.String(maxLength: 32),
                        Approver = c.String(maxLength: 32),
                        ApprovedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.QpNo, unique: true)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.QuotationProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(maxLength: 128),
                        ProductName = c.String(maxLength: 200),
                        CategoryName = c.String(maxLength: 50),
                        ProductEnName = c.String(maxLength: 200),
                        CnDescription = c.String(maxLength: 256),
                        EnDescription = c.String(maxLength: 256),
                        HSCODE = c.String(maxLength: 10),
                        HSADDTAXRATE = c.Decimal(precision: 18, scale: 2),
                        HSBACKTAXRATE = c.Decimal(precision: 18, scale: 2),
                        CUSTBASIC = c.String(),
                        GUIDEPRICE = c.Decimal(precision: 18, scale: 2),
                        Remark = c.String(maxLength: 256),
                        ThirdProductNo = c.String(maxLength: 128),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(maxLength: 10),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Cur = c.String(maxLength: 20),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USDAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RMBAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BrightcmsRate = c.Decimal(precision: 18, scale: 2),
                        BrightcmsFcy = c.Decimal(precision: 18, scale: 2),
                        DarkcmsRate = c.Decimal(precision: 18, scale: 2),
                        DarkcmsFcy = c.Decimal(precision: 18, scale: 2),
                        Executor = c.String(maxLength: 20),
                        Logo = c.String(maxLength: 256),
                        QpNo = c.String(nullable: false, maxLength: 20),
                        QuotationId = c.Int(nullable: false),
                        Ver = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .Index(t => t.QuotationId);
            
            AlterColumn("dbo.Products", "CnDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.Products", "EnDescription", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationProducts", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.QuotationFiles", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.Quotations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Quotations", "CompanyId", "dbo.Companies");
            DropIndex("dbo.QuotationProducts", new[] { "QuotationId" });
            DropIndex("dbo.Quotations", new[] { "CustomerId" });
            DropIndex("dbo.Quotations", new[] { "CompanyId" });
            DropIndex("dbo.Quotations", new[] { "QpNo" });
            DropIndex("dbo.QuotationFiles", new[] { "QuotationId" });
            AlterColumn("dbo.Products", "EnDescription", c => c.String());
            AlterColumn("dbo.Products", "CnDescription", c => c.String());
            DropTable("dbo.QuotationProducts");
            DropTable("dbo.Quotations");
            DropTable("dbo.QuotationFiles");
        }
    }
}
