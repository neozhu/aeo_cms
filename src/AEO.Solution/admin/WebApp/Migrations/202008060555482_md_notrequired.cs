namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_notrequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "CustomerCode" });
            AlterColumn("dbo.CustomerAttentionProducts", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.Customers", "CustomerCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.CustomerBanks", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerContacts", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerContacts", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerFiles", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerFiles", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerFollows", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerFollows", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerSales", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerSales", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerShares", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerShares", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerWarehouses", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerWarehouses", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerCommunications", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerCommunications", "CustomerName", c => c.String(maxLength: 80));
            AlterColumn("dbo.CustomerInvoices", "CustomerCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerInvoices", "CustomerName", c => c.String(maxLength: 80));
            CreateIndex("dbo.Customers", "CustomerCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "CustomerCode" });
            AlterColumn("dbo.CustomerInvoices", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerInvoices", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerCommunications", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerCommunications", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerWarehouses", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerWarehouses", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerShares", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerShares", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerSales", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerSales", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerFollows", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerFollows", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerFiles", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerFiles", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerContacts", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CustomerContacts", "CustomerCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CustomerBanks", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Customers", "CustomerCode", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.CustomerAttentionProducts", "CustomerName", c => c.String(nullable: false, maxLength: 80));
            CreateIndex("dbo.Customers", "CustomerCode", unique: true);
        }
    }
}
