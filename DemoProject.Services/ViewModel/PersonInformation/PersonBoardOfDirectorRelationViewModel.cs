using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonBoardOfDirectorRelationViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        public byte RelationPrmKey { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonBoardOfDirectorMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonBoardOfDirectorRelationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string NameOfPerson { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid BoardOfDirectorId { get; set; }

        [StringLength(20)]
        public string SequenceNumberText { get; set; }

        public Guid RelationId { get; set; }

        [StringLength(50)]
        public string NameOfRelation { get; set; }

    }
}
