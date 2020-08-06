namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_notrequired1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "CreditCode" });
            AlterColumn("dbo.Customers", "CreditCode", c => c.String(maxLength: 18));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CreditCode", c => c.String(nullable: false, maxLength: 18));
            CreateIndex("dbo.Customers", "CreditCode", unique: true);
        }
    }
}
