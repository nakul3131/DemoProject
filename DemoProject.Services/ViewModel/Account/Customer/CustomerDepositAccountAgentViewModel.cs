using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerDepositAccountAgentViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerDepositAccountAgentId { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int AgentPrmKey { get; set; }

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

        //CustomerDepositAccountAgentMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerDepositAccountAgentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }


        // DropdownList
        public Guid AgentId { get; set; }

        public string NameOfAgent { get; set; }

        // Other
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
