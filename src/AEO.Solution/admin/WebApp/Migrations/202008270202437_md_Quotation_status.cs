namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_Quotation_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "Status", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "Status");
        }
    }
}
