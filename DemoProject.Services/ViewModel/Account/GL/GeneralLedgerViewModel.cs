using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerViewModel
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public GeneralLedgerViewModel()
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        // GeneralLedger
        public short PrmKey { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(20)]
        public string GLCode { get; set; }

        [StringLength(20)]
        public string ExistingGLNumber { get; set; }

        public int GLNumber { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short AccountClassPrmKey { get; set; }

        public bool HasCustomerAccounts { get; set; }

        public bool IsApplicableForTax { get; set; }

        public bool EnableCashTransactionFromDemandDepositAccount { get; set; }

        [StringLength(1)]
        public string BusinessOfficeAccess { get; set; }

        [StringLength(1)]
        public string CurrencyAccess { get; set; }

        [StringLength(1)]
        public string TransactionTypeAccess { get; set; }

        [StringLength(1)]
        public string CustomerTypeAccess { get; set; }

        public short ParentGLPrmKey { get; set; }

        [StringLength(1500)]
        public string ParentGLDescription { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // GeneralLedgerMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // GeneralLedgerModification
        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // GeneralLedgerModificationMakerChecker

        public short GeneralLedgerModificationPrmKey { get; set; }

        // GeneralLedgerTranslation

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfGL { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransParentGLDescription { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // GeneralLedgerTranslationMakerChecker

        public short GeneralLedgerTranslationPrmKey { get; set; }

        public bool ContributionByDesignation { get; set; }

        public bool ContributionByGender { get; set; }

        // Other
        public Guid AccountClassId { get; set; }

        [StringLength(100)]
        public string NameOfAccountClass { get; set; }

        public Guid ParentGLId { get; set; }

        [StringLength(100)]
        public string NameOfParentGL { get; set; }

        public byte NumberOfBranches
        {
            get
            {
                return configurationDetailRepository.GetNumberOfBranches();
            }
        }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // GeneralLedgerBusinessOffice
        public GeneralLedgerBusinessOfficeViewModel GeneralLedgerBusinessOfficeViewModel { get; set; }

        // GeneralLedgerCurrency
        public GeneralLedgerCurrencyViewModel GeneralLedgerCurrencyViewModel { get; set; }

        // GeneralLedgerTransactionType
        public GeneralLedgerTransactionTypeViewModel GeneralLedgerTransactionTypeViewModel { get; set; }

        // GeneralLedgerCustomerType
        public GeneralLedgerCustomerTypeViewModel GeneralLedgerCustomerTypeViewModel { get; set; }
    }
}
