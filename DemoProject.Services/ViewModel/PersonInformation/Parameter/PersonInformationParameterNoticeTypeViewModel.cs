using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.PersonInformation.Parameter
{
    public class PersonInformationParameterNoticeTypeViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;

        public PersonInformationParameterNoticeTypeViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        // PersonInformationParameterNoticeType

        public short PrmKey { get; set; }

        //public Guid PersonInformationParameterNoticeTypeId { get; set; }

        public byte PersonInformationParameterPrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public bool EnableNoticeInRegionalLanguage { get; set; }

        public byte MaximumResendsOnFailure { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PersonInformationParameterNoticeTypeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short PersonInformationParameterNoticeTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem
        public Guid NoticeTypeId { get; set; }

        [StringLength(100)]
        public string NameOfNoticeType { get; set; }

        // List<SelectListItem> For Dropdown
        public List<SelectListItem> NoticeTypeDropdownList
        {
            get
            {
                return managementDetailRepository.NoticeTypeDropdownList;
            }
        }     
    }
}