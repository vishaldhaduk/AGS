using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.ViewModels
{
    public class MemberInfoViewModel
    {
        //MemberMaster
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
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string THome { get; set; }
        public string TBusiness { get; set; }
        public string TFax { get; set; }
        public bool NewsLetter { get; set; }
        public string MemberType { get; set; }

        //Account
        public bool Paid { get; set; }
        public string Amount { get; set; }
        public string DepositDate { get; set; }
        public string PaymentType { get; set; }
        public string Comment { get; set; }
    }
}