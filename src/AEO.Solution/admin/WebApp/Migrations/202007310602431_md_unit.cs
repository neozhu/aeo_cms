namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_unit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "InnerUnit", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "InnerUnit");
        }
    }
}
