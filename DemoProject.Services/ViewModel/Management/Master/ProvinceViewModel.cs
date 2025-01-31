using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ProvinceViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;

        public ProvinceViewModel()
        {
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // Province
        public short PrmKey { get; set; }
        
        public Guid ProvinceId { get; set; }
        
        public short Entity { get; set; }
        
        [StringLength(4000)]
        public string ProvinceOfMeeting { get; set; }

        public short SequenceNumber { get; set; }
        
        [StringLength(20)]
        public string SequenceNumberText { get; set; }

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

        // ProvinceMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short ProvincePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // ProvinceTranslation
        public Guid ProvinceTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(4000)]
        public string TransProvinceOfMeeting { get; set; }

        [StringLength(20)]
        public string TransSequenceNumberText { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        // ProvinceTranslationMakerChecker
        public short ProvinceTranslationPrmKey { get; set; }

        // ProvinceModification
        public Guid ProvinceModificationId { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // ProvinceModificationMakerChecker
        public short ProvinceModificationPrmKey { get; set; }

        // Translation In Regional
        [StringLength(4000)]
        public string ProvinceOfMeetingInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Province Of Meeting");
            }
        }

        [StringLength(100)]
        public string ProvinceOfMeetingPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Province Of Meeting");
            }
        }

        [StringLength(20)]
        public string SequenceNumberTextInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Sequence Number Text");
            }
        }

        [StringLength(100)]
        public string SequenceNumberTextPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Sequence Number Text");
            }
        }

        [StringLength(4000)]
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

        [StringLength(1500)]
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

        public DateTime MakerEntryDateTime { get; set; }

    }
}
