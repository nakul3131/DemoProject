using DemoProject.Services.Abstract.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class BoardOfDirectorAuthorizedSignatoryViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;

        public BoardOfDirectorAuthorizedSignatoryViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        // Added Designation Properties
        public short DesignationPrmKey { get; set; }

        public Guid DesignationId { get; set; }

        public bool IsAuthorizedSignatory { get; set; }


        [StringLength(50)]
        public string PersonInformationNumber { get; set; }

        public string PersonInformationNumberText { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] SignPhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPathSign { get; set; }

        [StringLength(1500)]
        public string SignStoragePath { get; set; }

        [StringLength(150)]
        public string FullNameOfAuthorizedPerson { get; set; }

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

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransFullNameOfAuthorizedPerson { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }
        public string DesignationText { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public List<SelectListItem> DesignationDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDesignationDropdownList;
            }
        }

    }
}
