using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountFacilityViewModel
    {
        //CustomerAccountFacility
        public short PrmKey { get; set; }

        public Guid CustomerAccountFacilityId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short FacilityPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //CustomerAccountFacilityMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short CustomerAccountFacilityPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList
        public Guid FacilityId { get; set; }

        public string NameOfFacility { get; set; }

        // Other
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
