using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingInvitee")]
    public partial class MeetingInviteeBoardOfDirector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingInviteeBoardOfDirector()
        {
            MeetingInviteeBoardOfDirectorMakerCheckers = new HashSet<MeetingInviteeBoardOfDirectorMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

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

        //public virtual Meeting Meeting { get; set; }

        public virtual BoardOfDirector BoardOfDirector { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingInviteeBoardOfDirectorMakerChecker> MeetingInviteeBoardOfDirectorMakerCheckers { get; set; }
    }
}
