using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonCommoditiesAssetViewModel
    {
        //PersonCommoditiesAsset
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public decimal GoldOrnaments { get; set; }

        public decimal SilverOrnaments { get; set; }

        public decimal PlatinumOrnaments { get; set; }

        public short NumberOfDiamondsInGoldOrnaments { get; set; }

        public bool HasAnyMortgage { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonCommoditiesAssetMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonCommoditiesAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Person
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        public Guid PersonId { get; set; }

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

    }
}
