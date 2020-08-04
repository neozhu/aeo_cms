namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changelength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Status", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Status", c => c.String(maxLength: 20));
        }
    }
}
