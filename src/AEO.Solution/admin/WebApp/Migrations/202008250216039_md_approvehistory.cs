namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_approvehistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApproveHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefId = c.Int(),
                        RekKey = c.String(maxLength: 128),
                        Status = c.String(maxLength: 32),
                        Initiator = c.String(maxLength: 32),
                        SubmitDate = c.DateTime(),
                        ToAuditor = c.String(maxLength: 32),
                        Approver = c.String(maxLength: 32),
                        ApprovedDate = c.DateTime(),
                        Result = c.String(maxLength: 512),
                        Comment = c.String(maxLength: 512),
                        Remark = c.String(maxLength: 512),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Inquiries", "Initiator", c => c.String(maxLength: 32));
            AddColumn("dbo.Inquiries", "SubmitDate", c => c.DateTime());
            AddColumn("dbo.Inquiries", "ToAuditor", c => c.String(maxLength: 32));
            AddColumn("dbo.Inquiries", "Approver", c => c.String(maxLength: 32));
            AddColumn("dbo.Inquiries", "ApprovedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inquiries", "ApprovedDate");
            DropColumn("dbo.Inquiries", "Approver");
            DropColumn("dbo.Inquiries", "ToAuditor");
            DropColumn("dbo.Inquiries", "SubmitDate");
            DropColumn("dbo.Inquiries", "Initiator");
            DropTable("dbo.ApproveHistories");
        }
    }
}
