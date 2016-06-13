using AdminGujaratiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL.Repository
{
    public class NonMemberEntryRepository : Repository<NonMemberEntryMaster>
    {
        public NonMemberEntryRepository(AGSDBContext context) : base(context)
        {

        }

        public List<int> GetMember(int mID)
        {
            List<int> mIDs = new List<int>();
            IEnumerable<NonMemberEntryMaster> cm = context.NonMemberEntryMasters.Where(p => p.ID == mID);
            return mIDs;
        }
    }
}