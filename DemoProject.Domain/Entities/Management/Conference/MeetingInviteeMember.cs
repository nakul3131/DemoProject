using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingInviteeMember")]
    public partial class MeetingInviteeMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingInviteeMember()
        {
            MeetingInviteeMemberMakerCheckers = new HashSet<MeetingInviteeMemberMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public int CustomerSharesCapitalAccountPrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NoticeReferenceNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string NoticeStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string AttendanceStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Meeting Meeting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingInviteeMemberMakerChecker> MeetingInviteeMemberMakerCheckers { get; set; }
    }
}
