namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberMaster", "Title", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "Phone1", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "Phone2", c => c.String());
            AddColumn("dbo.MemberDetailMaster", "MemberType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberDetailMaster", "MemberType");
            DropColumn("dbo.MemberDetailMaster", "Phone2");
            DropColumn("dbo.MemberDetailMaster", "Phone1");
            DropColumn("dbo.MemberMaster", "Title");
        }
    }
}
