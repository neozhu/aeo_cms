namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductPurchaseHistoricalPrices", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductSalesHistoricalPrices", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductPurchaseHistoricalPrices", "SaluPric");
            DropColumn("dbo.ProductSalesHistoricalPrices", "SaluPric");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductSalesHistoricalPrices", "SaluPric", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductPurchaseHistoricalPrices", "SaluPric", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductSalesHistoricalPrices", "UnitPrice");
            DropColumn("dbo.ProductPurchaseHistoricalPrices", "UnitPrice");
        }
    }
}
