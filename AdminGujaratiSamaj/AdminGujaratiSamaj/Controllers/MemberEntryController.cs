using AdminGujaratiSamaj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminGujaratiSamaj.Controllers
{
    public class MemberEntryController : ApiController
    {
        MemberEntryViewModel[] member = new MemberEntryViewModel[]
        {
            new MemberEntryViewModel{MemberID=425,BarcodeId="425+525",IsPrimary=true,FamilyId=525,Paid=true,Amount="15"},
            new MemberEntryViewModel{MemberID=426,BarcodeId="426+525",IsPrimary=false,FamilyId=525,Paid=true,Amount="15"},
            new MemberEntryViewModel{MemberID=427,BarcodeId="427+525",IsPrimary=false,FamilyId=525,Paid=false,Amount=""},
            new MemberEntryViewModel{MemberID=428,BarcodeId="428+525",IsPrimary=false,FamilyId=525,Paid=true,Amount="15"}
        };

        public IEnumerable<MemberEntryViewModel> GetAllMembers()
        {
            return member;
        }

        public IHttpActionResult GetMembers(string barcode)
        {
            var m = member.FirstOrDefault((p) => p.BarcodeId == barcode);
            if (m == null)
            {
                return NotFound();
            }
            return Ok(m);
        }
    }
}
