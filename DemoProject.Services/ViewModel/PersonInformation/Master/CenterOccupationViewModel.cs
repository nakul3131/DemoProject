using DemoProject.Services.Abstract.PersonInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterOccupationViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public CenterOccupationViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        // CenterOccupation

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterOccupationId { get; set; }

        public short CenterPrmKey { get; set; }

        public short OccupationPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CenterOccupationMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short CenterOccupationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Center

        public Guid CenterId { get; set; }

        [StringLength(100)]
        public string NameOfCenter { get; set; }

        // Category i.e. Occupation
        public Guid OccupationId { get; set; }

        // NameOfCategory i.e. NameOfOccupation

        [StringLength(100)]
        public string NameOfCategory { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        public Guid[] SelectedOccupationId { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        //Dropdown
        public List<SelectListItem> OccupationDropdownList
        {
            get
            {
                return personDetailRepository.OccupationDropdownList;
            }
        }
    }
}
