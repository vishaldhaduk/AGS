using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class MemberAccountMaster
    {
        public int ID { get; set; }
        [ForeignKey("MemberMaster")]
        public int MemberID { get; set; }
        public bool Paid { get; set; }
        public string Amount { get; set; }
        public string DepositDate { get; set; }
        public string PaymentType { get; set; }
        public string Comment { get; set; }
        public virtual MemberMaster MemberMaster { get; set; }
    }
}