namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changeCustomerAttentionProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerAttentionProducts", "Category", c => c.String(maxLength: 128));
            AddColumn("dbo.CustomerAttentionProducts", "CategoryId", c => c.Int());
            AlterColumn("dbo.CustomerAttentionProducts", "ProductNo", c => c.String(maxLength: 128));
            AlterColumn("dbo.CustomerBanks", "Remark", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerBanks", "Remark", c => c.String());
            AlterColumn("dbo.CustomerAttentionProducts", "ProductNo", c => c.String(maxLength: 50));
            DropColumn("dbo.CustomerAttentionProducts", "CategoryId");
            DropColumn("dbo.CustomerAttentionProducts", "Category");
        }
    }
}
