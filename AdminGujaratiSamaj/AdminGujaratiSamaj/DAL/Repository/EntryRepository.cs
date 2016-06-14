using AdminGujaratiSamaj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL.Repository
{
    public class EntryRepository : Repository<EntryMaster>
    {
        public EntryRepository(AGSDBContext context) : base(context)
        {

        }

        public List<int> GetMember(int eID)
        {
            List<int> eIDs = new List<int>();
            IEnumerable<EntryMaster> cm = context.EntryMasters.Where(p => p.ID == eID);
            return eIDs;
        }
    }
}