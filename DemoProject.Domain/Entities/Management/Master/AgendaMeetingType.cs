using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("AgendaMeetingType")]
    public partial class AgendaMeetingType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgendaMeetingType()
        {
            AgendaMeetingTypeMakerCheckers = new HashSet<AgendaMeetingTypeMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid AgendaMeetingTypeId { get; set; }

        public int AgendaPrmKey { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaMeetingTypeMakerChecker> AgendaMeetingTypeMakerCheckers { get; set; }
    }
}
