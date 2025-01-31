using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class AuthorizedSharesCapitalViewModel
    {
        // AuthorizedSharesCapital

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid AuthorizedSharesCapitalId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime AuthorizedDate { get; set; }

        public decimal AuthorizedSharesCapitalAmount { get; set; }
        
        [StringLength(50)]
        public string ReferenceNumber { get; set; }
        
        [StringLength(4000)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; } 

        //AuthorizedSharesCapitalMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte AuthorizedSharesCapitalPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public string NameOfMaker { get; set; }

        public DateTime MakerEntryDateTime { get; set; }
    
    }

}
