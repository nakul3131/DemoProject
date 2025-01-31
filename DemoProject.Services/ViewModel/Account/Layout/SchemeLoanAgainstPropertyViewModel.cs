using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanAgainstPropertyViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte MinimumLTVRatio { get; set; }

        public byte MaximumLTVRatio { get; set; }

        public bool IsAllowResidentialProperty { get; set; }

        public bool IsAllowCommericalProperty { get; set; }

        public bool IsAllowAgricultureProperty { get; set; }

        [StringLength(1)]
        public string PropertyInsurance { get; set; }

         [StringLength(1500)]
        public string LocatedAreaRemark { get; set; }

         [StringLength(1500)]
        public string Note { get; set; }

         [StringLength(3)]
        public string EntryStatus { get; set; }
        
        //SchemeLoanAgainstPropertyMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanAgainstPropertyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
        
    }
}
