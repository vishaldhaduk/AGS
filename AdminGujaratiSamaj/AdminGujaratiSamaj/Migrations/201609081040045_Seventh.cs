namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EntryMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.MemberDetailMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.MemberManageMaster", "MemberMasterID", "dbo.MemberMaster");
            DropPrimaryKey("dbo.MemberMaster");
            AlterColumn("dbo.MemberMaster", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MemberMaster", "ID");
            AddForeignKey("dbo.EntryMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MemberDetailMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MemberManageMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberManageMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.MemberDetailMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.EntryMaster", "MemberMasterID", "dbo.MemberMaster");
            DropPrimaryKey("dbo.MemberMaster");
            AlterColumn("dbo.MemberMaster", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MemberMaster", "ID");
            AddForeignKey("dbo.MemberManageMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MemberDetailMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EntryMaster", "MemberMasterID", "dbo.MemberMaster", "ID", cascadeDelete: true);
        }
    }
}
