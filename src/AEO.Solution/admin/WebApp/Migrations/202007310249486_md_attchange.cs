namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_attchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "Size", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Attachments", "FileName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Attachments", "FileId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Attachments", "Ext", c => c.String(maxLength: 10));
            AlterColumn("dbo.Attachments", "RefKey", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachments", "RefKey", c => c.String(maxLength: 100));
            AlterColumn("dbo.Attachments", "Ext", c => c.String(maxLength: 100));
            AlterColumn("dbo.Attachments", "FileId", c => c.String(maxLength: 100));
            AlterColumn("dbo.Attachments", "FileName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Attachments", "Size");
        }
    }
}
