using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonMasterViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public PersonMasterViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonInformationNumber { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string MothersMaidenName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //PersonMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonPrefix

        //public Guid PersonPrefixId { get; set; }

        public byte ModificationNumber { get; set; }

        public byte PrefixPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //PersonPrefixMakerChecker

        //PersonModificationMakerChecker

        public long PersonModificationPrmKey { get; set; }

        //PersonTranslation

        //public Guid PersonTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransFirstName { get; set; }

        [StringLength(50)]
        public string TransMiddleName { get; set; }

        [StringLength(50)]
        public string TransLastName { get; set; }

        [StringLength(150)]
        public string TransFullName { get; set; }

        [StringLength(50)]
        public string TransMotherName { get; set; }

        [StringLength(50)]
        public string TransMothersMaidenName { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //PersonTranslationMAkerChecker

        public long PersonTranslationPrmKey { get; set; }


        public DateTime DateOfBirthOnDocument { get; set; }

        public bool EnableGSTRegistrationDetails { get; set; }


        // PersonPrefixViewModel
        public PersonPrefixViewModel PersonPrefixViewModel { get; set; }

        // PersonEmploymentDetailViewModel
        public PersonEmploymentDetailViewModel PersonEmploymentDetailViewModel { get; set; }


        // GuardianPersonViewModel
        public GuardianPersonViewModel GuardianPersonViewModel { get; set; }

        // PersonAdditionalDetailViewModel
        public PersonAdditionalDetailViewModel PersonAdditionalDetailViewModel { get; set; }

        // PersonAddressViewModel
        public PersonAddressViewModel PersonAddressViewModel { get; set; }


        // PersonGSTRegistrationDetailViewModel
        public PersonGSTRegistrationDetailViewModel PersonGSTRegistrationDetailViewModel { get; set; }


        // PersonKYCDocumentViewModel
        public PersonKYCDocumentViewModel PersonKYCDocumentViewModel { get; set; }

        // Other
        public Guid LanguageId { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }



        // Translation In Regional
        [StringLength(100)]
        public string PrefixInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Prefix");
            }
        }

        [StringLength(100)]
        public string PrefixPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Prefix");
            }
        }

        [StringLength(100)]
        public string FirstNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("First Name");
            }
        }

        [StringLength(100)]
        public string FirstNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter First Name");
            }
        }

        [StringLength(100)]
        public string MiddleNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Middle Name");
            }
        }

        [StringLength(100)]
        public string MiddleNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Middle Name");
            }
        }

        [StringLength(100)]
        public string LastNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Last Name");
            }
        }

        [StringLength(100)]
        public string LastNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Last Name");
            }
        }

        [StringLength(100)]
        public string MotherNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Mother Name");
            }
        }

        [StringLength(100)]
        public string MotherNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Mother Name");
            }
        }

        [StringLength(100)]
        public string MothersMaidenNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Mothers Maiden Name");
            }
        }

        [StringLength(100)]
        public string MothersMaidenNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Mothers Maiden Name");
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


        public Guid PrefixId { get; set; }

        // List<SelectListItem> For Dropdown

        public List<SelectListItem> PrefixDropdownList
        {
            get
            {
                return personDetailRepository.PrefixDropdownList;
            }
        }

    }
}
