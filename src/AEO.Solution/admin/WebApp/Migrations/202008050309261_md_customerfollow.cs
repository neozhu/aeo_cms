namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_customerfollow : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerFollows", "Content", c => c.String(maxLength: 512));
            AlterColumn("dbo.CustomerFollows", "ReminderContent", c => c.String(maxLength: 512));
            AlterColumn("dbo.CustomerFollows", "ReminderTo", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerFollows", "ReminderTo", c => c.String(maxLength: 200));
            AlterColumn("dbo.CustomerFollows", "ReminderContent", c => c.String());
            AlterColumn("dbo.CustomerFollows", "Content", c => c.String());
        }
    }
}
