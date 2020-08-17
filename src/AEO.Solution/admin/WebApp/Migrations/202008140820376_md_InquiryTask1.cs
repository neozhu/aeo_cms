namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_InquiryTask1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InquiryTasks", "TaskNo", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InquiryTasks", "TaskNo", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
