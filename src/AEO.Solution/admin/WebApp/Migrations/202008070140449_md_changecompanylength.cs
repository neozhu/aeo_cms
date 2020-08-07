namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changecompanylength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Ctype", c => c.String(maxLength: 128));
            AlterColumn("dbo.Companies", "LegalPerson", c => c.String(maxLength: 56));
            AlterColumn("dbo.Companies", "Contect", c => c.String(maxLength: 56));
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(maxLength: 56));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Companies", "Contect", c => c.String(maxLength: 12));
            AlterColumn("dbo.Companies", "LegalPerson", c => c.String(maxLength: 12));
            AlterColumn("dbo.Companies", "Ctype", c => c.String(maxLength: 56));
        }
    }
}
