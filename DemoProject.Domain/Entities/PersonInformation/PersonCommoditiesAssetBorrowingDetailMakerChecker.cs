using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonCommoditiesAssetBorrowingDetailMakerChecker")]
    public partial class PersonCommoditiesAssetBorrowingDetailMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public long PersonCommoditiesAssetBorrowingDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual PersonCommoditiesAssetBorrowingDetail PersonCommoditiesAssetBorrowingDetail { get; set; }
    }
}
