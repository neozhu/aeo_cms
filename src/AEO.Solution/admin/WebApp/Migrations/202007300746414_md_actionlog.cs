namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_actionlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefId = c.Int(nullable: false),
                        RekKey = c.String(maxLength: 128),
                        ActionDateTime = c.DateTime(nullable: false),
                        User = c.String(maxLength: 20),
                        Action = c.String(maxLength: 20),
                        Content = c.String(maxLength: 128),
                        Flag = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.ProductPurchaseHistoricalPrices", "ProductNo", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProductSalesHistoricalPrices", "ProductNo", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ProductPacks", "ProductNo");
            DropColumn("dbo.ProductPacks", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductPacks", "ProductName", c => c.String(maxLength: 200));
            AddColumn("dbo.ProductPacks", "ProductNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductSalesHistoricalPrices", "ProductNo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ProductPurchaseHistoricalPrices", "ProductNo", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.ActionLogs");
        }
    }
}
