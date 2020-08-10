namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changeaeotest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AeoQuestions", "Short", c => c.String(maxLength: 128));
            AddColumn("dbo.QuestionTpls", "Short", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionTpls", "Short");
            DropColumn("dbo.AeoQuestions", "Short");
        }
    }
}
