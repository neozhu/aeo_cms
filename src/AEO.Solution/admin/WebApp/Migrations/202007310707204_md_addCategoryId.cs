namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addCategoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CategoryId");
        }
    }
}
