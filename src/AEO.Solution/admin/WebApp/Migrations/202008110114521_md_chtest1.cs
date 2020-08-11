namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_chtest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AeoAuthTests", "TestNo", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.AeoQuestions", "TestNo", c => c.String(maxLength: 20));
            AlterColumn("dbo.AeoAuthTests", "Name", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AeoAuthTests", "Name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AeoQuestions", "TestNo");
            DropColumn("dbo.AeoAuthTests", "TestNo");
        }
    }
}
