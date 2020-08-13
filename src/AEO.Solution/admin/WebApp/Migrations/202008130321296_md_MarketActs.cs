namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_MarketActs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarketActs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Owner = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 20),
                        ActType = c.String(nullable: false, maxLength: 56),
                        PlanStartDate = c.DateTime(),
                        PlanFinishDate = c.DateTime(),
                        BudgetExpense = c.Decimal(precision: 18, scale: 2),
                        Cur = c.String(maxLength: 56),
                        Address = c.String(maxLength: 128),
                        PlanDesc = c.String(maxLength: 512),
                        ActualStartDate = c.DateTime(),
                        ActualFinishDate = c.DateTime(),
                        ActExpense = c.Decimal(precision: 18, scale: 2),
                        Income = c.Decimal(precision: 18, scale: 2),
                        ExecDesc = c.String(maxLength: 512),
                        SumaryDesc = c.String(maxLength: 512),
                        EffectDesc = c.String(maxLength: 512),
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
            DropTable("dbo.MarketActs");
        }
    }
}
