using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Domain.Entities.Management.SystemEntity;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingNotice")]
    public partial class MeetingNotice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingNotice()
        {
            MeetingNoticeMakerCheckers = new HashSet<MeetingNoticeMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public short NoticeMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        public int MenuPrmKey { get; set; } 

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Meeting Meeting { get; set; } 

        public virtual NoticeMedia NoticeMedia { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual Menu Menu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingNoticeMakerChecker> MeetingNoticeMakerCheckers { get; set; }
    }
}
