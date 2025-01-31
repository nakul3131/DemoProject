using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountNomineeViewModel
    {
        public long PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(50)]
        public string NominationNumber { get; set; }

        public DateTime NominationDate { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(150)]
        public string NameOfNominee { get; set; }

        public long NomineePersonInformationNumber { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(500)]
        public string FullAddressDetails { get; set; }

        [StringLength(150)]
        public string ContactDetails { get; set; }

        public byte RelationPrmKey { get; set; }

        public decimal HoldingPercentage { get; set; }

        public decimal ProportionateAmountForEachNominee { get; set; }

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

        // CustomerAccountNomineeMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountNomineePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerAccountNomineeTranslation
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransNameOfNominee { get; set; }

        [StringLength(500)]
        public string TransFullAddressDetails { get; set; }

        [StringLength(150)]
        public string TransContactDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // CustomerAccountNomineeTranslationMakerChecker
        public long CustomerAccountNomineeTranslationPrmKey { get; set; }

        // CustomerAccountNomineeGuardianViewModel
        public CustomerAccountNomineeGuardianViewModel CustomerAccountNomineeGuardianViewModel { get; set; }

        public IEnumerable<CustomerAccountNomineeGuardianViewModel> CustomerAccountNomineeGuardianViewModelList { get; set; }

        //Other
        public byte CustomerId { get; set; }

        [StringLength(150)]
        public string NameOfCustomer { get; set; }

        public int NomineeAge { get; set; }

        // DropdownList

        public Guid PersonId { get; set; }

        [StringLength(150)]
        public string NameOfPerson { get; set; }

        public Guid RelationId { get; set; }

        public string NameOfRelation { get; set; }

        public string NameOfPersonInformationNumber { get; set; }

    }
}
