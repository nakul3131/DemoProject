using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("DaysOfWeek")]
    public partial class DaysOfWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaysOfWeek()
        {
            DaysOfWeekTranslations = new HashSet<DaysOfWeekTranslation>();
            ScheduleFrequencies = new HashSet<ScheduleFrequency>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid DaysOfWeekId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfWeekDay { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DaysOfWeekTranslation> DaysOfWeekTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleFrequency> ScheduleFrequencies { get; set; }
    }
}
