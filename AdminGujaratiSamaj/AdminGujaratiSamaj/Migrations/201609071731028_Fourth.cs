namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberDetailMaster", "DOB", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberDetailMaster", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
