using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public class NonMemberEntryMaster
    {
        public int ID { get; set; }
        public bool Paid { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}