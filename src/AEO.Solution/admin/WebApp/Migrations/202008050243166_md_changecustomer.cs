namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changecustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BaseName", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "Telephone", c => c.String(maxLength: 50));
            AddColumn("dbo.Customers", "Fax", c => c.String(maxLength: 50));
            AddColumn("dbo.Customers", "Owner", c => c.String(maxLength: 20));
            AddColumn("dbo.Customers", "Payment", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "ContactName", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.Customers", "Appellation", c => c.String(maxLength: 10));
            AddColumn("dbo.Customers", "Sex", c => c.String(maxLength: 10));
            AddColumn("dbo.Customers", "Job", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.Customers", "Wx", c => c.String(maxLength: 50));
            AddColumn("dbo.Customers", "PhoneNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.Customers", "ContactRemark", c => c.String(maxLength: 20));
            AddColumn("dbo.Customers", "LastContactDate", c => c.DateTime());
            AddColumn("dbo.CustomerContacts", "Owner", c => c.String(maxLength: 20));
            AddColumn("dbo.CustomerContacts", "Wx", c => c.String(maxLength: 50));
            AddColumn("dbo.CustomerContacts", "PhoneNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "MasterCustom", c => c.String(maxLength: 4));
            AlterColumn("dbo.Customers", "Remark", c => c.String(maxLength: 512));
            DropColumn("dbo.Customers", "CustomerEName");
            DropColumn("dbo.Customers", "CURR");
            DropColumn("dbo.Customers", "CashCURR");
            DropColumn("dbo.Customers", "Remark1");
            DropColumn("dbo.CustomerContacts", "PhoneNumber3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerContacts", "PhoneNumber3", c => c.String(maxLength: 50));
            AddColumn("dbo.Customers", "Remark1", c => c.String(maxLength: 256));
            AddColumn("dbo.Customers", "CashCURR", c => c.String(maxLength: 20));
            AddColumn("dbo.Customers", "CURR", c => c.String(maxLength: 32));
            AddColumn("dbo.Customers", "CustomerEName", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Remark", c => c.String());
            AlterColumn("dbo.Customers", "MasterCustom", c => c.String(maxLength: 10));
            DropColumn("dbo.CustomerContacts", "PhoneNumber");
            DropColumn("dbo.CustomerContacts", "Wx");
            DropColumn("dbo.CustomerContacts", "Owner");
            DropColumn("dbo.Customers", "LastContactDate");
            DropColumn("dbo.Customers", "ContactRemark");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "PhoneNumber");
            DropColumn("dbo.Customers", "Wx");
            DropColumn("dbo.Customers", "Job");
            DropColumn("dbo.Customers", "Sex");
            DropColumn("dbo.Customers", "Appellation");
            DropColumn("dbo.Customers", "ContactName");
            DropColumn("dbo.Customers", "Payment");
            DropColumn("dbo.Customers", "Owner");
            DropColumn("dbo.Customers", "Fax");
            DropColumn("dbo.Customers", "Telephone");
            DropColumn("dbo.Customers", "BaseName");
        }
    }
}
