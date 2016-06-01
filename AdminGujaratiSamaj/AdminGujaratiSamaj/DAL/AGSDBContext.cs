using AdminGujaratiSamaj.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL
{
    public class AGSDBContext: IdentityDbContext<ApplicationUser>
    {
        public AGSDBContext(): base("AGSDBContext")
        {

        }

        public DbSet<EntryMaster> EntryMasters { get; set; }
        public DbSet<MemberMaster> MemberMasters { get; set; }
        public DbSet<MemberDetailMaster> MemberDetailMasters { get; set; }
        public DbSet<MemberManageMaster> MemberManageMasters { get; set; }
        public DbSet<NonMemberEntryMaster> NonMemberEntryMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}