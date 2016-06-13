using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class EntryMaster
    {
        public int ID { get; set; }
        //[ForeignKey("AdminGujaratiSamaj.Models.MemberMaster")]
        public int MemberMasterID { get; set; }
        public bool Paid { get; set; }
        public string SeatNo { get; set; }
        public bool DiwaliPass { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public virtual MemberMaster MemberMaster { get; set; }
    }
}