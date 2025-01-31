using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerJointAccountHolderViewModel
    {
        // CustomerJointAccount
        public int PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonPrmKey { get; set; }

        public byte JointAccountHolderTypePrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // CustomerAccountJointAccountHolderMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerJointAccountHolderPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }


        // DropdownList
        public string FullName { get; set; }

        public Guid PersonId { get; set; }

        public Guid JointAccountHolderTypeId { get; set; }

        public string NameOfJointAccountHolderType { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

    }
}
