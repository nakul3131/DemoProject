using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Conference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MeetingAllowanceViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public MeetingAllowanceViewModel() 
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // MeetingAllowance

        public int PrmKey { get; set; }

        public Guid MeetingAllowanceId { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        [StringLength(50)]
        public string NameOfAllowance { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(1500)]
        public string ShortDescription { get; set; }

        public bool IsDailyAllowance { get; set; }

        public bool IsRequiredBill { get; set; }

        public decimal MinimumAllowanceAmount { get; set; }

        public decimal MaximumAllowanceAmount { get; set; }

        public decimal DefaultAllowanceAmount { get; set; }

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

        // MeetingAllowanceMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MeetingAllowancePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // MeetingAllowanceModification

        public Guid MeetingAllowanceModificationId { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // MeetingAllowanceModificationMakerChecker

        public int MeetingAllowanceModificationPrmKey { get; set; }

        // MeetingAllowanceTranslation

        public Guid MeetingAllowanceTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransNameOfAllowance { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransShortDescription { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        // MeetingAllowanceTranslationMakerChecker

        public int MeetingAllowanceTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfAllowanceInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Allowance");
            }
        }

        [StringLength(100)]
        public string NameOfAllowancePlaceHolderInRegionalLanguage 
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Allowance");
            }
        }

        [StringLength(100)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(1500)]
        public string ShortDescriptionInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Short Description");
            }
        }

        [StringLength(1500)]
        public string ShortDescriptionPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Short sDescription");
            }
        }

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

        public Guid MeetingTypeId { get; set; } 

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public List<SelectListItem> MeetingTypeDropdownList
        {
            get
            {
                return managementDetailRepository.MeetingTypeDropdownList;
            }
        }
         
    }
}
