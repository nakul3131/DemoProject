using DemoProject.Services.Abstract.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonGroupAuthorizedSignatoryViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;

        public PersonGroupAuthorizedSignatoryViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        public long PrmKey { get; set; }

        public long PersonGroupPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        // Added Designation Properties
        public short DesignationPrmKey { get; set; }

        public Guid DesignationId { get; set; }

        public bool IsAuthorizedSignatory { get; set; }

        public long PersonInformationNumber { get; set; }

        public string PersonInformationNumberText { get; set; }

        [StringLength(150)]
        public string SignNameOfFile { get; set; }

        [StringLength(150)]
        public string SignFileCaption { get; set; }

        public byte[] Sign { get; set; }

        public HttpPostedFileBase PhotoPathSign { get; set; }

        [StringLength(1500)]
        public string SignLocalStoragePath { get; set; }

        [StringLength(150)]
        public string FullNameOfAuthorizedPerson { get; set; }
       
        [StringLength(1500)]
        public string AuthorizedPersonAddressDetail { get; set; }

        [StringLength(1500)]
        public string AuthorizedPersonContactDetail { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        // Translation
        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransFullNameOfAuthorizedPerson { get; set; }


        [StringLength(1500)]
        public string TransAuthorizedPersonAddressDetail { get; set; }


        [StringLength(1500)]
        public string TransAuthorizedPersonContactDetail { get; set; }


        [StringLength(1500)]
        public string TransNote { get; set; }

        // PersonInformation

        [StringLength(1)]
        public string SignDocumentUpload { get; set; }

        public bool EnableSignDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInDb { get; set; }

        public bool EnableSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public string NameOfDesignation { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        //For MakerChecker Relation
        public long PersonGroupAuthorizedSignatoryPrmKey { get; set; }

        public long PersonGroupAuthorizedSignatoryTranslationPrmKey { get; set; }
        public Guid PersonGroupId { get; set; }
        public PersonGroupViewModel PersonGroupViewModel { get; set; }


        public List<SelectListItem> DesignationDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDesignationDropdownList;
            }
        }

    }
}
