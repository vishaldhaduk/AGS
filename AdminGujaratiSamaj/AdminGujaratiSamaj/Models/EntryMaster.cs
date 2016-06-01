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
        [ForeignKey("MemberMaster")]
        public int MemberID { get; set; }
        public bool Paid { get; set; }
        public string SeatNo { get; set; }
        public bool DiwaliPass { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}