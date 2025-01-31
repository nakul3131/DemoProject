using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Conference;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerSharesCapitalAccount")]
    public partial class CustomerSharesCapitalAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerSharesCapitalAccount()
        {
            CustomerSharesCapitalAccountMakerCheckers = new HashSet<CustomerSharesCapitalAccountMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public int MemberNumber { get; set; }

        public bool IsOtherSocietyMember { get; set; }

        public bool IsCompletedCooperativeEducation { get; set; }

        [Required]
        [StringLength(3)]
        public string MembershipStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual MinuteOfMeetingAgenda MinuteOfMeetingAgenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSharesCapitalAccountMakerChecker> CustomerSharesCapitalAccountMakerCheckers { get; set; }
    }
}
