using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class SharesApplicationViewModel
    {
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISharesApplicationRepository sharesApplicationRepository;

        public SharesApplicationViewModel()
        {
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            sharesApplicationRepository = DependencyResolver.Current.GetService<ISharesApplicationRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // SharesApplication

        public int PrmKey { get; set; }

        public DateTime ApplicationAllotmentDate { get; set; }

        public DateTime ApplicationSubmitDate { get; set; }
     
        public long ApplicationNumber { get; set; }

        public bool HasOtherSocietyMembership { get; set; }

        [StringLength(150)]
        public string WitnessName { get; set; }

        [StringLength(500)]
        public string BankDetails { get; set; }

        public decimal TransactionAmount { get; set; }

        [StringLength(50)]
        public string UniqueTransactionNumber { get; set; }

        [StringLength(3)]
        public string ApplicationStatus { get; set; }

        [StringLength(1500)]
        public string StatusReason { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SharesApplicationMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int SharesApplicationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // SharesApplicationModification

        public Guid SharesApplicationModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // SharesApplicationModificationMakerChecker

        public int SharesApplicationModificationPrmKey { get; set; }

        // SharesApplicationTranslation

        public Guid SharesApplicationTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransWitnessName { get; set; }

        [StringLength(500)]
        public string TransBankDetails { get; set; }
      
        [StringLength(1500)]
        public string TransStatusReason { get; set; }
        
        
        [StringLength(1500)]
        public string TransNote { get; set; }

        // SharesApplicationTranslationMakerChecker

        public int SharesApplicationTranslationPrmKey { get; set; }

        // SharesApplicationDetail

        public Guid SharesApplicationDetailId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte MemberTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        // SharesApplicationDetailMakerChecker

        public short SharesApplicationDetailPrmKey { get; set; }

        public SharesApplicationDetailViewModel SharesApplicationDetailViewModel { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(100)]
        public string BankDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Bank Details");
            }
        }

        [StringLength(100)]
        public string BankDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Bank Details");
            }
        }

        [StringLength(100)]
        public string WitnessNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Witness Name");
            }
        }

        [StringLength(100)]
        public string WitnessNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Witness");
            }
        }


        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }
               
        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime? MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }   
    }
}
