using DemoProject.Services.Abstract.PersonInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class GuardianViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public GuardianViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        public long PrmKey { get; set; }

        public Guid GuardianPersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(1)]
        public string AgeProofSubmissionStatusOfTheMinor { get; set; }

        [Required]
        [StringLength(150)]
        public string GuardianFullName { get; set; }

        public int Relation { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //Guardian Translation

        public Guid GuardianPersonTranslationId { get; set; }

        public long GuardianPersonPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransGuardianFullName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }
        
        //Maker Checker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public long GuardianPersonTranslationPrmKey { get; set; }

        // Other

        [Required]
        [StringLength(50)]
        public string NameOfUser { get; set; } 

        public DateTime MakerEntryDateTime { get; set; }

        // For SelectListItem

        public Guid RelationId { get; set; }

        public Guid GuardianTypeId { get; set; }

        // DropdownList

        public List<SelectListItem> RelationDropdownList
        {
            get
            {
                return personDetailRepository.RelationDropdownList;
            }

        }

        public List<SelectListItem> GuardianTypeDropdownList
        {
            get
            {
                return personDetailRepository.GuardianTypeDropdownList;
            }

        }

    }
}
