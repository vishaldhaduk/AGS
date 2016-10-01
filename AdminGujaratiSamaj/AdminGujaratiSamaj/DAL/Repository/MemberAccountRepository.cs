using AdminGujaratiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL.Repository
{
    public class MemberAccountRepository : Repository<MemberAccountMaster>
    {
        public MemberAccountRepository(AGSDBContext context)
            : base(context)
        {

        }

        internal IEnumerable<MemberAccountMaster> GetMemberAccountDetail(int? id)
        {
            var m = context.MemberAccountMasters.Where(p => p.MemberID == id);
            return m;
        }
    }
}