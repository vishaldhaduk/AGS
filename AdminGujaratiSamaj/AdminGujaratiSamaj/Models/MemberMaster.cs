using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.Models
{
    public partial class MemberMaster
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string BarcodeId { get; set; }
        public bool IsPrimary { get; set; }
        public int FamilyId { get; set; }
        public string Title { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
    }

    [MetadataType(typeof(UsemetaMetadata))]
    public partial class MemberMaster
    {
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return (FName + " " + LName).Trim();
            }
        }

        public class UsemetaMetadata
        {
        }
    }
}