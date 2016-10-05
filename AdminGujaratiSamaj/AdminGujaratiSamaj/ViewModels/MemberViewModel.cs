using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.ViewModels
{
    public class MemberViewModel
    {
        public int MemberID { get; set; }
        public string BarcodeId { get; set; }
        public bool IsPrimary { get; set; }
        public int FamilyId { get; set; }
        public string Title { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return (Title + " " + FName + " " + LName).Trim();
            }
        }

        //MemberDetails
        public string MemberType { get; set; }

        //Account
        public bool Paid { get; set; }
        public string Amount { get; set; }
    }
}