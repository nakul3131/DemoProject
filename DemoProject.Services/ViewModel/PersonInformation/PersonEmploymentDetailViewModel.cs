using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonEmploymentDetailViewModel
    {
        // PersonEmploymentDetail
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(150)]
        public string NameOfEmployer { get; set; }

        public DateTime DateOfIncorporation { get; set; }

        public byte EmploymentTypePrmKey { get; set; }

        public byte EmployerNaturePrmKey { get; set; }

        [StringLength(1500)]
        public string EmployerNatureOtherDetails { get; set; }

        [StringLength(1500)]
        public string EmployerAddressDetails { get; set; }

        [StringLength(500)]
        public string EmployerContactDetails { get; set; }

        public short EmployerCityPrmKey { get; set; }

        public short DesignationPrmKey { get; set; }

        public decimal AnnualIncome { get; set; }

        [StringLength(50)]
        public string EPFNumber { get; set; }

        public short EmployedSince { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonEmploymentDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonEmploymentDetailPrmKey { get; set; }

        public long CustomerAccountEmploymentDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }
                
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNameOfEmployer { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransEmployerNatureOtherDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransEmployerAddressDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string TransEmployerContactDetails { get; set; }

        [Required]
        [StringLength(50)]
        public string TransEPFNumber { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //PersonEmploymentDetailTranslationMakerChecker
        public long PersonEmploymentDetailTranslationPrmKey { get; set; }

        // Translation In Regional

        public Guid OccupationId { get; set; }

        public Guid EmploymentTypeId { get; set; }

        public Guid NatureOfEmployerId { get; set; }

        public Guid CityId { get; set; }

        public Guid DesignationId { get; set; }

    }
}
