namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mdchtest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AeoQuestions", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.AeoQuestions", "TestDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AeoQuestions", "TestDateTime");
            DropColumn("dbo.AeoQuestions", "Score");
        }
    }
}
