using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class ForeignerViewModel
    {
        public long PrmKey { get; set; }

       // public Guid ForeignerPersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool IsPermanentThisCountryResidentStatus { get; set; }

        public bool IsVisitedThisCountryInLast3Years { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //Maker Checker
        
        public DateTime EntryDateTime { get; set; }

        public long ForeignerPersonPrmKey { get; set; }

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

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }
    }
}
