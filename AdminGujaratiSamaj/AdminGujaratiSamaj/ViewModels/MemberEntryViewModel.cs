using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.ViewModels
{
    public class MemberEntryViewModel
    {
        public int MemberID { get; set; }
        public string BarcodeId { get; set; }
        public bool IsPrimary { get; set; }
        public int FamilyId { get; set; }
        public bool Paid { get; set; }
        public string Amount { get; set; }
    }
}