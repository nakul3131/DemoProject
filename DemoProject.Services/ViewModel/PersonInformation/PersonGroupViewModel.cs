using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonGroupViewModel
    {
        // PersonGroup
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(3)]
        public string BusinessType { get; set; }

        [StringLength(3)]
        public string BusinessNature { get; set; }

        public DateTime DateOfEstablishment { get; set; }

        [StringLength(150)]
        public string BusinessRegistrationNumber { get; set; }
       
        [StringLength(150)]
        public string OtherRegistrationNumber { get; set; }

        public bool HasAnyAssociatedCompanies { get; set; }
        
        [StringLength(1500)]
        public string AssociatedCompaniesList { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //PersonGroupMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonGroupPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }


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
        

        //Person
        public Guid PersonId { get; set; }

    }
}
