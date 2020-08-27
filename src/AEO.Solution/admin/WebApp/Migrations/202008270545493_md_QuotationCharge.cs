namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_QuotationCharge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuotationCharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        EName = c.String(maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            DropForeignKey("dbo.QuotationCharges", "QuotationId", "dbo.Quotations");
            DropIndex("dbo.QuotationCharges", new[] { "QuotationId" });
            DropTable("dbo.QuotationCharges");
        }
    }
}
