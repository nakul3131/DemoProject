using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleTranslation")]
    public partial class NoticeScheduleTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleTranslation()
        {
            NoticeScheduleTranslationMakerCheckers = new HashSet<NoticeScheduleTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfNoticeSchedule { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual NoticeSchedule NoticeSchedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleTranslationMakerChecker> NoticeScheduleTranslationMakerCheckers { get; set; }
    }
}
