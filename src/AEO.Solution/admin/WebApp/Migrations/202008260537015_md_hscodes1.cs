namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_hscodes1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HSCodes", "im_low_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "im_normal_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "im_temp_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "im_tax_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "im_consume_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "ex_return_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "ex_normal_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "ex_temp_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "ex_special_rate", c => c.String(maxLength: 56));
            AlterColumn("dbo.HSCodes", "ex_tax_rate", c => c.String(maxLength: 56));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HSCodes", "ex_tax_rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "ex_special_rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "ex_temp_rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "ex_normal_rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "ex_return_rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "im_consume_rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "im_tax_rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "im_temp_rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "im_normal_rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HSCodes", "im_low_rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
