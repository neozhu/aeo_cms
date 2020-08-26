namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_QuotationHistories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuotationHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 12),
                        ProductNo = c.String(maxLength: 128),
                        ProductName = c.String(maxLength: 200),
                        TargetPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompetitorPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Summary = c.String(maxLength: 256),
                        RecordDate = c.DateTime(nullable: false),
                        Owner = c.String(maxLength: 20),
                        QuotationProductId = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationHistories", "QuotationId", "dbo.Quotations");
            DropIndex("dbo.QuotationHistories", new[] { "QuotationId" });
            DropTable("dbo.QuotationHistories");
        }
    }
}
