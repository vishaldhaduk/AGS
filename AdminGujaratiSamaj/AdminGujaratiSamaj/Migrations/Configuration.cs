namespace AdminGujaratiSamaj.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminGujaratiSamaj.DAL.AGSDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdminGujaratiSamaj.DAL.AGSDBContext context)
        {
            var member = new List<MemberMaster>
            {
                new MemberMaster{LName="Patel",FName="Alex",BarcodeId="D7-0128", FamilyId="0128",IsPrimary=true},
                new MemberMaster{LName="Patel",FName="Paul",BarcodeId="D7-0129", FamilyId="0128",IsPrimary=false},
                new MemberMaster{LName="Patel",FName="Boris",BarcodeId="D7-0130", FamilyId="0128",IsPrimary=false},
                new MemberMaster{LName="Shah",FName="Danton",BarcodeId="D7-0131", FamilyId="0131",IsPrimary=true},
                new MemberMaster{LName="Shah",FName="Elena",BarcodeId="D7-0132", FamilyId="0131",IsPrimary=false}
                //new MemberMaster{ID=1, LName="Carson",FName="Alexander",BarcodeId="D7-0128", FamilyId="0128",IsPrimary=true},
            };

            member.ForEach(c => context.MemberMasters.AddOrUpdate(p => p.FName, c));
            context.SaveChanges();

            var memberdetail = new List<MemberDetailMaster>
            {
                new MemberDetailMaster{MemberMasterID=1, Address="At Home 1", DOB=DateTime.Now, Email="abc1@abc.com", NewsLetter=true, Phone="15142323451",Sex="Male"},
                new MemberDetailMaster{MemberMasterID=2, Address="At Home 2", DOB=DateTime.Now, Email="abc2@abc.com", NewsLetter=true, Phone="25142323451",Sex="Male"},
                new MemberDetailMaster{MemberMasterID=3, Address="At Home 3", DOB=DateTime.Now, Email="abc3@abc.com", NewsLetter=true, Phone="35142323451",Sex="Male"},
                new MemberDetailMaster{MemberMasterID=4, Address="At Home 4", DOB=DateTime.Now, Email="abc4@abc.com", NewsLetter=true, Phone="45142323451",Sex="Male"},
                new MemberDetailMaster{MemberMasterID=5, Address="At Home 5", DOB=DateTime.Now, Email="abc5@abc.com", NewsLetter=true, Phone="55142323451",Sex="Famale"}
            };
            memberdetail.ForEach(c => context.MemberDetailMasters.AddOrUpdate(p => p.MemberMasterID, c));
            context.SaveChanges();

            var entry = new List<EntryMaster>
            {
                new EntryMaster{MemberMasterID=1,Date=DateTime.Now, DiwaliPass=false, Paid=false, Comment="1Alexander bought it",SeatNo="SG-21"},
                new EntryMaster{MemberMasterID=2,Date=DateTime.Now, DiwaliPass=false, Paid=false, Comment="2Alexander bought it",SeatNo="SG-22"},
                new EntryMaster{MemberMasterID=3,Date=DateTime.Now, DiwaliPass=true, Paid=true, Comment="3Alexander bought it",SeatNo="SG-23"},
                new EntryMaster{MemberMasterID=4,Date=DateTime.Now, DiwaliPass=true, Paid=true, Comment="4Alexander bought it",SeatNo="SG-24"},
                new EntryMaster{MemberMasterID=5,Date=DateTime.Now, DiwaliPass=false, Paid=false, Comment="5Alexander bought it",SeatNo="SG-25"}
            };
            entry.ForEach(c => context.EntryMasters.AddOrUpdate(p => p.MemberMasterID, c));
            context.SaveChanges();

            var membermanage = new List<MemberManageMaster>
            {
                new MemberManageMaster{MemberMasterID=2,PMemberID=1},
                new MemberManageMaster{MemberMasterID=3,PMemberID=1},
                new MemberManageMaster{MemberMasterID=5,PMemberID=4}

            };
            membermanage.ForEach(c => context.MemberManageMasters.AddOrUpdate(p => p.MemberMasterID, c));
            context.SaveChanges();

            var nonmember = new List<NonMemberEntryMaster>
            {
                new NonMemberEntryMaster{Paid=true,Date=DateTime.Now,Comment="Vishal Patel bought it for his GF"},
                new NonMemberEntryMaster{Paid=true,Date=DateTime.Now,Comment="Jay thakkar bought it for his GF"},
                new NonMemberEntryMaster{Paid=false,Date=DateTime.Now,Comment="Harsh Shah bought it for his GF"}
            };
            nonmember.ForEach(c => context.NonMemberEntryMasters.AddOrUpdate(p => p.Comment, c));
            context.SaveChanges();


            base.Seed(context);
        }
    }
}
