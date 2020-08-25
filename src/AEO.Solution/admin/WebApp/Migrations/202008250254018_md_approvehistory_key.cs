namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_approvehistory_key : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApproveHistories", "RefKey", c => c.String(maxLength: 128));
            DropColumn("dbo.ApproveHistories", "RekKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApproveHistories", "RekKey", c => c.String(maxLength: 128));
            DropColumn("dbo.ApproveHistories", "RefKey");
        }
    }
}
