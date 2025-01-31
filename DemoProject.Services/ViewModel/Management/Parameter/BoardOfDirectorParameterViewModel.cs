using DemoProject.Services.Abstract.Management.Parameter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Management.Parameter
{
    public class BoardOfDirectorParameterViewModel
    {
        private readonly IAssuranceDeedFormatRepository assuranceDeedFormatRepository;

        public BoardOfDirectorParameterViewModel()
        {
            assuranceDeedFormatRepository = DependencyResolver.Current.GetService<IAssuranceDeedFormatRepository>();
        }

        // BoardOfDirectorParameter

        public short PrmKey { get; set; }

        public Guid BoardOfDirectorParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte TotalNumberOfDirectors { get; set; }

        public byte BoardOfDirectorValidity { get; set; }

        public byte ReserveSeatsForGeneralCategory { get; set; }

        public byte ReserveSeatsForSCST { get; set; }

        public byte ReserveSeatsForWeakerSection { get; set; }

        public byte ReserveSeatsForOBC { get; set; }

        public byte ReserveSeatsForWomen { get; set; }

        public byte ReserveSeatsForDNotifiedTribes { get; set; }

        public byte ReserveSeatsForCooptExpertDirector { get; set; }

        public bool IsRequiredActiveMemberForDirector { get; set; }

        public byte AssuranceDeedFormat { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DirectorAndTheirRelativesMortgageLoanLimitAgainstTotalLoanInPercentage { get; set; }

        public decimal DirectorAndTheirRelativesMortgageLoanLimitAgainstTotalLoanAmount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BoardOfDirectorParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BoardOfDirectorParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public Guid AssuranceDeedFormatId { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public List<SelectListItem> AssuranceDeedFormatDropdownList
        {
            get
            {
                return assuranceDeedFormatRepository.AssuranceDeedFormatDropdownList;
            }
        }
    }
}
