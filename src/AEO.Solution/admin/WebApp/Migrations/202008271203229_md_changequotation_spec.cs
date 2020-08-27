namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changequotation_spec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuotationProducts", "Spec", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuotationProducts", "Spec");
        }
    }
}
