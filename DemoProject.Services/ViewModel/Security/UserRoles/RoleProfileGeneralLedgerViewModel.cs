using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.UserRoles
{
    public class RoleProfileGeneralLedgerViewModel
    {
        public int PrmKey { get; set; }

        public Guid RoleProfileGeneralLedgerId { get; set; }

        public short RoleProfilePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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

        //RoleProfileGeneralLedgerMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int RoleProfileGeneralLedgerPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }
         
        // For DropDowns
        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public Guid[] MultiGeneralLedgerId { get; set; }
    }
}
