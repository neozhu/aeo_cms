namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_aeotest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AeoAuthTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        TradeCode = c.String(maxLength: 10),
                        CreditCode = c.String(maxLength: 18),
                        Ctype = c.String(maxLength: 128),
                        AuthType = c.String(maxLength: 128),
                        MasterCustom = c.String(maxLength: 10),
                        RegistDate = c.DateTime(),
                        IsForeign = c.String(maxLength: 50),
                        Zone = c.String(maxLength: 128),
                        RegistedTime = c.Decimal(precision: 18, scale: 2),
                        Unit = c.String(maxLength: 10),
                        AuthDate = c.DateTime(),
                        Tester = c.String(maxLength: 28),
                        Year = c.Int(),
                        BeginDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Remark = c.String(maxLength: 512),
                        Status = c.String(maxLength: 12),
                        StdScore = c.Decimal(precision: 18, scale: 2),
                        Score = c.Decimal(precision: 18, scale: 2),
                        Result = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AeoQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tpl = c.String(nullable: false, maxLength: 128),
                        AuthType = c.String(maxLength: 128),
                        Category = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 128),
                        Code = c.String(maxLength: 12),
                        Title = c.String(maxLength: 128),
                        StdDescription = c.String(maxLength: 256),
                        Notes = c.String(maxLength: 128),
                        StdScore = c.Int(nullable: false),
                        ScoreDescription = c.String(maxLength: 256),
                        Remark = c.String(maxLength: 128),
                        Tester = c.String(maxLength: 28),
                        AeoAuthTestId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AeoAuthTests", t => t.AeoAuthTestId, cascadeDelete: true)
                .Index(t => t.AeoAuthTestId);
            
            CreateTable(
                "dbo.QuestionTpls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tpl = c.String(nullable: false, maxLength: 128),
                        AuthType = c.String(nullable: false, maxLength: 128),
                        Category = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 128),
                        Code = c.String(maxLength: 12),
                        Title = c.String(maxLength: 128),
                        StdDescription = c.String(maxLength: 256),
                        Notes = c.String(maxLength: 128),
                        StdScore = c.Int(nullable: false),
                        ScoreDescription = c.String(maxLength: 256),
                        Remark = c.String(maxLength: 128),
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
            DropForeignKey("dbo.AeoQuestions", "AeoAuthTestId", "dbo.AeoAuthTests");
            DropIndex("dbo.AeoQuestions", new[] { "AeoAuthTestId" });
            DropTable("dbo.QuestionTpls");
            DropTable("dbo.AeoQuestions");
            DropTable("dbo.AeoAuthTests");
        }
    }
}
