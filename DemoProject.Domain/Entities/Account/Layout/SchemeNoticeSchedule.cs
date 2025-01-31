using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeNoticeSchedule")]
    public partial class SchemeNoticeSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeNoticeSchedule()
        {
            SchemeNoticeScheduleMakerCheckers = new HashSet<SchemeNoticeScheduleMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public byte CommunicationMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeNoticeScheduleMakerChecker> SchemeNoticeScheduleMakerCheckers { get; set; }
    }
}
