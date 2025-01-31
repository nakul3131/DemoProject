using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterDemographicDetailViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public CenterDemographicDetailViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        // CenterDemographicDetail

        public short PrmKey { get; set; }

        public Guid CenterDemographicDetailId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte LocalGovernmentPrmKey { get; set; }

        public byte DirectionPrmKey { get; set; }

        public byte AreaTypePrmKey { get; set; }

        public long TotalPopulation { get; set; }

        public decimal PerCapitaIncome { get; set; }

        public byte EducationLevelPrmKey { get; set; }

        public byte FamilySystemPrmKey { get; set; }

        public long NumberOfResidentsOwningHomes { get; set; }

        public int Pincode { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CenterDemographicDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short CenterDemographicDetailPrmKey { get; set; }

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

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // Dropdown

        public Guid AreaTypeId { get; set; }

        public Guid DirectionId { get; set; }

        public Guid EducationLevelId { get; set; }

        public Guid FamilySystemId { get; set; }

        public Guid LocalGovernmentId { get; set; }

        public List<SelectListItem> AreaTypeDropdownList
        {
            get
            {
                return personDetailRepository.AreaTypeDropdownList;
            }
        }

        public List<SelectListItem> DirectionDropdownList
        {
            get
            {
                return personDetailRepository.DirectionDropdownList;
            }
        }

        public List<SelectListItem> EducationLevelDropdownList
        {
            get
            {
                return personDetailRepository.EducationLevelDropdownList;
            }
        }

        public List<SelectListItem> FamilySystemDropdownList
        {
            get
            {
                return personDetailRepository.FamilySystemDropdownList;
            }
        }

        public List<SelectListItem> LocalGovernmentDropdownList
        {
            get
            {
                return personDetailRepository.LocalGovernmentDropdownList;
            }
        }
    }
}
