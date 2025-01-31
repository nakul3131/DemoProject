using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Master;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CountryAdditionalDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public CountryAdditionalDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        // CountryAdditionalDetail

        [Key]
        public short PrmKey { get; set; }

        public Guid CountryAdditionalDetailId { get; set; }

        public byte ModificationNumber { get; set; }

        public short CenterPrmKey { get; set; }

        public byte MinorAge { get; set; }

        public short WorldWideTimeZonePrmKey { get; set; }

        [Range(0, 999, ErrorMessage = "Dialing Codes Maximum length must be 999")]
        public short InternationalDialingCodes { get; set; }

        public short CurrencyPrmKey { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CountryAdditionalDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short CountryAdditionalDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Center

        public Guid CenterId { get; set; }

        [StringLength(100)]
        public string NameOfCenter { get; set; }

        // Other

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // Dropdown

        public Guid CurrencyId { get; set; }

        public Guid ParentCenterId { get; set; }

        public Guid WorldWideTimeZoneId { get; set; }


        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }

        }

        public List<SelectListItem> SubContinentDropdownList
        {
            get
            {
                return personDetailRepository.SubContinentDropdownList;
            }
        }

        public List<SelectListItem> WorldWideTimeZoneDropdownList
        {
            get
            {
                return personDetailRepository.WorldWideTimeZoneDropdownList;
            }
        }
    }
}
