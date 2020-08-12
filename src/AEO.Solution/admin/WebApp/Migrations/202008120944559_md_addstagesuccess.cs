namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addstagesuccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OpportunityStages", "Success", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OpportunityStages", "Success");
        }
    }
}
