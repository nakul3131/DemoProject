using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("DesignationModification")]
    public partial class DesignationModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DesignationModification()
        {
            DesignationModificationMakerCheckers = new HashSet<DesignationModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short DesignationPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string DesignationCategory { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfDesignation { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short SequenceNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DesignationModificationMakerChecker> DesignationModificationMakerCheckers { get; set; }
    }
}
