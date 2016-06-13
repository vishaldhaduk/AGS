using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class MemberManageMaster
    {
        public int ID { get; set; }
        //[ForeignKey("MemberMaster")]
        public int MemberMasterID { get; set; }
        public int PMemberID { get; set; }
        public virtual MemberMaster MemberMaster { get; set; }
    }
}