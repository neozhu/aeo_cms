namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_product : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "ProductNo" });
            AddColumn("dbo.Products", "Category", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Spec", c => c.String(maxLength: 100));
            AlterColumn("dbo.Products", "ProductNo", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "Flag1", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Flag2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductFiles", "FileName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProductFiles", "Size", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ProductFiles", "Folder", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProductFiles", "FileId", c => c.String(maxLength: 38));
            AlterColumn("dbo.ProductFiles", "Ext", c => c.String(maxLength: 10));
            AlterColumn("dbo.ProductPrictures", "FileName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProductPrictures", "Description", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProductPrictures", "LineNo", c => c.Int());
            AlterColumn("dbo.ProductPrictures", "FileId", c => c.String(maxLength: 38));
            CreateIndex("dbo.Products", "ProductNo", unique: true);
            DropColumn("dbo.Products", "ProductNature");
            DropColumn("dbo.Products", "ProductNoAlias");
            DropColumn("dbo.Products", "Model");
            DropColumn("dbo.Products", "CategoryName");
            DropColumn("dbo.Products", "Flag3");
            DropColumn("dbo.Products", "Group");
            DropColumn("dbo.Products", "Status1");
            DropColumn("dbo.Products", "Status2");
            DropColumn("dbo.Products", "Status3");
            DropColumn("dbo.Products", "Attribute1");
            DropColumn("dbo.Products", "Attribute2");
            DropColumn("dbo.Products", "Attribute3");
            DropColumn("dbo.Products", "Attribute4");
            DropColumn("dbo.Products", "Attribute5");
            DropColumn("dbo.Products", "Attribute6");
            DropColumn("dbo.Products", "Attribute7");
            DropColumn("dbo.Products", "Attribute8");
            DropColumn("dbo.Products", "ProductNoAlias2");
            DropColumn("dbo.Products", "ProductNoAlias3");
            DropColumn("dbo.Products", "Group2");
            DropColumn("dbo.Products", "Group3");
            DropColumn("dbo.Products", "Group4");
            DropColumn("dbo.Products", "CompanyCode");
            DropColumn("dbo.Products", "CompanyName");
            DropColumn("dbo.Products", "CompanyId");
            DropColumn("dbo.ProductFiles", "RefKey");
            DropColumn("dbo.ProductFiles", "ProductNo");
            DropColumn("dbo.ProductFiles", "ProductName");
            DropColumn("dbo.ProductPrictures", "Ext");
            DropColumn("dbo.ProductPrictures", "Owner");
            DropColumn("dbo.ProductPrictures", "Upload");
            DropColumn("dbo.ProductPrictures", "ProductNo");
            DropColumn("dbo.ProductPrictures", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductPrictures", "ProductName", c => c.String(maxLength: 200));
            AddColumn("dbo.ProductPrictures", "ProductNo", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductPrictures", "Upload", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductPrictures", "Owner", c => c.String(maxLength: 20));
            AddColumn("dbo.ProductPrictures", "Ext", c => c.String(maxLength: 100));
            AddColumn("dbo.ProductFiles", "ProductName", c => c.String(maxLength: 200));
            AddColumn("dbo.ProductFiles", "ProductNo", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductFiles", "RefKey", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "CompanyId", c => c.Int());
            AddColumn("dbo.Products", "CompanyName", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "CompanyCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Products", "Group4", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Group3", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Group2", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "ProductNoAlias3", c => c.String(maxLength: 200));
            AddColumn("dbo.Products", "ProductNoAlias2", c => c.String(maxLength: 200));
            AddColumn("dbo.Products", "Attribute8", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute7", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute6", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute5", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute4", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute3", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute2", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Attribute1", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Status3", c => c.String(maxLength: 20));
            AddColumn("dbo.Products", "Status2", c => c.String(maxLength: 20));
            AddColumn("dbo.Products", "Status1", c => c.String(maxLength: 20));
            AddColumn("dbo.Products", "Group", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Flag3", c => c.String(maxLength: 20));
            AddColumn("dbo.Products", "CategoryName", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "Model", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "ProductNoAlias", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "ProductNature", c => c.String(maxLength: 50));
            DropIndex("dbo.Products", new[] { "ProductNo" });
            AlterColumn("dbo.ProductPrictures", "FileId", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductPrictures", "LineNo", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductPrictures", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductPrictures", "FileName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ProductFiles", "Ext", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductFiles", "FileId", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductFiles", "Folder", c => c.String(maxLength: 20));
            AlterColumn("dbo.ProductFiles", "Size", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProductFiles", "FileName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Flag2", c => c.String(maxLength: 20));
            AlterColumn("dbo.Products", "Flag1", c => c.String(maxLength: 20));
            AlterColumn("dbo.Products", "ProductNo", c => c.String(maxLength: 50));
            DropColumn("dbo.Products", "Spec");
            DropColumn("dbo.Products", "Category");
            CreateIndex("dbo.Products", "ProductNo", unique: true);
        }
    }
}
