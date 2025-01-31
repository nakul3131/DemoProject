using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeConsumerDurableLoanItemViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public decimal Margin { get; set; }
       
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeConsumerDurableLoanItemPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid ConsumerDurableItemId { get; set; }

        [StringLength(150)]
        public string NameOfItem { get; set; }
    }
}
