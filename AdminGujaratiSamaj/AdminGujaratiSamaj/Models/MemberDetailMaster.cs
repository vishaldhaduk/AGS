﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class MemberDetailMaster
    {
        public int ID { get; set; }
        //[ForeignKey("MemberMaster")]
        public int MemberMasterID { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string THome { get; set; }
        public string TBusiness { get; set; }
        public string TFax{ get; set; }
        public bool NewsLetter { get; set; }
        public virtual MemberMaster MemberMaster { get; set; }
        public string MemberType { get; set; }
    }
}