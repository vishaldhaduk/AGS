using AdminGujaratiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL.Repository
{
    public class MemberRepository : Repository<MemberMaster>
    {
        public MemberRepository(AGSDBContext context) : base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        internal IEnumerable<MemberMaster> GetNames(string term)
        {
            var members = context.MemberMasters.Where(p => p.FName.StartsWith(term));
            return members;
        }

        public List<int> GetMember(int mID)
        {
            List<int> mIDs = new List<int>();
            IEnumerable<MemberMaster> cm = context.MemberMasters.Where(p => p.ID == mID);
            return mIDs;
        }
    }
}