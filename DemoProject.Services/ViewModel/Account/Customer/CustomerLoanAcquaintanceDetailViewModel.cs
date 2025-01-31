using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanAcquaintanceDetailViewModel
    {
        //CustomerLoanAcquaintanceDetail

        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        public byte RelationPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerLoanAcquaintanceDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAcquaintanceDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
        public string NameOfRelation { get; set; }

        public string NameOfAcquaintance { get; set; }

        //Other
        public Guid RelationId { get; set; }
    }
}
