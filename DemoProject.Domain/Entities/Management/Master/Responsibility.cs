using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("Responsibility")]
    public partial class Responsibility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Responsibility()
        {
            ResponsibilityMakerCheckers = new HashSet<ResponsibilityMakerChecker>();
            ResponsibilityModifications = new HashSet<ResponsibilityModification>();
            ResponsibilityTranslations = new HashSet<ResponsibilityTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid ResponsibilityId { get; set; }

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

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsibilityMakerChecker> ResponsibilityMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsibilityModification> ResponsibilityModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsibilityTranslation> ResponsibilityTranslations { get; set; }
    }
}
