﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class MemberAccountMaster
    {
        public int ID { get; set; }
        //[ForeignKey("MemberMaster")]
        public int MemberMasterID { get; set; }
        public bool Paid { get; set; }
        public string Amount { get; set; }
        public string DepositDate { get; set; }
        public string PaymentType { get; set; }
        public string Comment { get; set; }
    }
}