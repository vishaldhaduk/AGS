namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberAccountMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberMasterID = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Amount = c.String(),
                        DepositDate = c.String(),
                        PaymentType = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberAccountMaster");
        }
    }
}
