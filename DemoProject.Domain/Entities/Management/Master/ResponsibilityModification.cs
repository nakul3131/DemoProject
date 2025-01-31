using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("ResponsibilityModification")]
    public partial class ResponsibilityModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponsibilityModification()
        {
            ResponsibilityModificationMakerCheckers = new HashSet<ResponsibilityModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short ResponsibilityPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfResponsibility { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short SequenceNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string SequenceText { get; set; }

        public short ParentFunctionPrmKey { get; set; }

        public bool IsTitle { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Responsibility Responsibility { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsibilityModificationMakerChecker> ResponsibilityModificationMakerCheckers { get; set; }
    }
}
