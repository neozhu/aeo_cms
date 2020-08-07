namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changecompanylength1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 50));
        }
    }
}
