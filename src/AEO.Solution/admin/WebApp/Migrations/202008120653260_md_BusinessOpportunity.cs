namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_BusinessOpportunity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessOpportunities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Owner = c.String(nullable: false, maxLength: 20),
                        CustomerId = c.Int(nullable: false),
                        ContactName = c.String(nullable: false, maxLength: 80),
                        OpDate = c.DateTime(nullable: false),
                        ProvidePeople = c.String(maxLength: 80),
                        Source = c.String(nullable: false, maxLength: 50),
                        MarketAction = c.String(maxLength: 128),
                        Status = c.String(maxLength: 50),
                        Curr = c.String(maxLength: 50),
                        PrDate = c.DateTime(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Content = c.String(maxLength: 512),
                        Stage = c.String(maxLength: 128),
                        StageDate = c.DateTime(),
                        Remark = c.String(maxLength: 128),
                        CustomerCode = c.String(maxLength: 20),
                        CustomerName = c.String(maxLength: 80),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.OpportunityStages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stage = c.String(maxLength: 128),
                        SuccessRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConfirmDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 128),
                        BusinessOpportunityId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessOpportunities", t => t.BusinessOpportunityId, cascadeDelete: true)
                .Index(t => t.BusinessOpportunityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpportunityStages", "BusinessOpportunityId", "dbo.BusinessOpportunities");
            DropForeignKey("dbo.BusinessOpportunities", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OpportunityStages", new[] { "BusinessOpportunityId" });
            DropIndex("dbo.BusinessOpportunities", new[] { "CustomerId" });
            DropTable("dbo.OpportunityStages");
            DropTable("dbo.BusinessOpportunities");
        }
    }
}
