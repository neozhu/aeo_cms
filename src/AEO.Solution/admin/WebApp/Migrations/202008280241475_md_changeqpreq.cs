namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changeqpreq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuotationProducts", "QpNo", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuotationProducts", "QpNo", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
