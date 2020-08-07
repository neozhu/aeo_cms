namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_company_required : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Companies", new[] { "CreditCode" });
            AlterColumn("dbo.Companies", "CreditCode", c => c.String(maxLength: 18));
            CreateIndex("dbo.Companies", "CreditCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Companies", new[] { "CreditCode" });
            AlterColumn("dbo.Companies", "CreditCode", c => c.String(nullable: false, maxLength: 18));
            CreateIndex("dbo.Companies", "CreditCode", unique: true);
        }
    }
}
