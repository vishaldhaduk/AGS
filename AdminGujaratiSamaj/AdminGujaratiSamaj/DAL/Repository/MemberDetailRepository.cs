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
        public MemberDetailRepository(AGSDBContext context) : base(context)
        {

        }

        internal IEnumerable<MemberDetailMaster> GetMemberDetail(int? id)
        {
            var m = context.MemberDetailMasters.Where(p => p.MemberMasterID == id);
            return m;
        }

        //internal MemberDetailMaster GetMemberDetail(int? id)
        //{
        //    //    MemberDetailMaster m = context.MemberDetailMasters.Where(p => p.MemberMasterID == id);
        //    //    return m;
        //}
    }
}