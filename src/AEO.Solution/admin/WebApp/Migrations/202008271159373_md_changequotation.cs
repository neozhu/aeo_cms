namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changequotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuotationProducts", "SupplierCode", c => c.String(maxLength: 20));
            AddColumn("dbo.QuotationProducts", "SupplierName", c => c.String(maxLength: 128));
            AddColumn("dbo.QuotationProducts", "SupplierProductNo", c => c.String(maxLength: 128));
            AddColumn("dbo.QuotationProducts", "IntPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.QuotationProducts", "PriceType", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuotationProducts", "PriceType");
            DropColumn("dbo.QuotationProducts", "IntPrice");
            DropColumn("dbo.QuotationProducts", "SupplierProductNo");
            DropColumn("dbo.QuotationProducts", "SupplierName");
            DropColumn("dbo.QuotationProducts", "SupplierCode");
        }
    }
}
