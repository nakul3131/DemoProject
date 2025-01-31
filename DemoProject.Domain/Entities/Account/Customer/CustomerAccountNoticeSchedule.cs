using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{

    [Table("CustomerAccountNoticeSchedule")]
    public partial class CustomerAccountNoticeSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountNoticeSchedule()
        {
            CustomerAccountNoticeScheduleMakerCheckers = new HashSet<CustomerAccountNoticeScheduleMakerChecker>();
        }
        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public byte CommunicationMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNoticeScheduleMakerChecker> CustomerAccountNoticeScheduleMakerCheckers { get; set; }
    }
}