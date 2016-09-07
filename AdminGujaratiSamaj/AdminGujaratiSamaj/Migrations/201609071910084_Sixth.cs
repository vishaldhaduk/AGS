namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberDetailMaster", "THome", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "TBusiness", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "TFax", c => c.String());
            AlterColumn("dbo.MemberMaster", "FamilyId", c => c.Int(nullable: false));
            DropColumn("dbo.MemberDetailMaster", "Phone");
            DropColumn("dbo.MemberDetailMaster", "Phone1");
            DropColumn("dbo.MemberDetailMaster", "Phone2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberDetailMaster", "Phone2", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "Phone1", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "Phone", c => c.String());
            AlterColumn("dbo.MemberMaster", "FamilyId", c => c.String());
            DropColumn("dbo.MemberDetailMaster", "TFax");
            DropColumn("dbo.MemberDetailMaster", "TBusiness");
            DropColumn("dbo.MemberDetailMaster", "THome");
        }
    }
}
