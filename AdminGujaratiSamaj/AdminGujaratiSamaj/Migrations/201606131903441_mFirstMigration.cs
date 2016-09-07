namespace AdminGujaratiSamaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mFirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntryMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberMasterID = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        SeatNo = c.String(),
                        DiwaliPass = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberMaster", t => t.MemberMasterID, cascadeDelete: true)
                .Index(t => t.MemberMasterID);
            
            CreateTable(
                "dbo.MemberMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: false),
                        BarcodeId = c.String(),
                        LName = c.String(),
                        FName = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        FamilyId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MemberDetailMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberMasterID = c.Int(nullable: false),
                        Address = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Sex = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        NewsLetter = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberMaster", t => t.MemberMasterID, cascadeDelete: true)
                .Index(t => t.MemberMasterID);
            
            CreateTable(
                "dbo.MemberManageMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberMasterID = c.Int(nullable: false),
                        PMemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberMaster", t => t.MemberMasterID, cascadeDelete: true)
                .Index(t => t.MemberMasterID);
            
            CreateTable(
                "dbo.NonMemberEntryMaster",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Paid = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.MemberManageMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.MemberDetailMaster", "MemberMasterID", "dbo.MemberMaster");
            DropForeignKey("dbo.EntryMaster", "MemberMasterID", "dbo.MemberMaster");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.MemberManageMaster", new[] { "MemberMasterID" });
            DropIndex("dbo.MemberDetailMaster", new[] { "MemberMasterID" });
            DropIndex("dbo.EntryMaster", new[] { "MemberMasterID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.NonMemberEntryMaster");
            DropTable("dbo.MemberManageMaster");
            DropTable("dbo.MemberDetailMaster");
            DropTable("dbo.MemberMaster");
            DropTable("dbo.EntryMaster");
        }
    }
}
