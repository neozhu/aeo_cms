namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_hscodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HSCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        hscode = c.String(nullable: false, maxLength: 10),
                        cn_name = c.String(maxLength: 512),
                        en_name = c.String(maxLength: 256),
                        g_model = c.String(maxLength: 256),
                        unit_code = c.String(maxLength: 3),
                        unit_name = c.String(maxLength: 12),
                        unit2_code = c.String(maxLength: 3),
                        unit2_name = c.String(maxLength: 12),
                        control_ma = c.String(maxLength: 256),
                        ciq_ma = c.String(maxLength: 256),
                        im_low_rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        im_normal_rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        im_temp_rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        im_tax_rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        im_consume_rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ex_return_rate = c.Decimal(precision: 18, scale: 2),
                        ex_normal_rate = c.Decimal(precision: 18, scale: 2),
                        ex_temp_rate = c.Decimal(precision: 18, scale: 2),
                        ex_special_rate = c.Decimal(precision: 18, scale: 2),
                        ex_tax_rate = c.Decimal(precision: 18, scale: 2),
                        remark = c.String(maxLength: 512),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HSCodes");
        }
    }
}
