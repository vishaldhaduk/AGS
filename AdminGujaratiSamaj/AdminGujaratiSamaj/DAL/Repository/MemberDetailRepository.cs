using AdminGujaratiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AdminGujaratiSamaj.DAL.Repository
{
    public class MemberDetailRepository : Repository<MemberDetailMaster>
    {
        public MemberDetailRepository(AGSDBContext context)
            : base(context)
        {

        }

        internal IEnumerable<MemberDetailMaster> GetMemberDetail(int? id)
        {
            var m = context.MemberDetailMasters.Where(p => p.MemberID == id);
            return m;
        }
        internal MemberDetailMaster GetMembersDetail(int? id)
        {
            using (AGSDBContext context =new AGSDBContext())
            {
                MemberDetailMaster m = context.MemberDetailMasters.Where(p => p.MemberID == id).FirstOrDefault();
                return m;
            }
        }
    }
}