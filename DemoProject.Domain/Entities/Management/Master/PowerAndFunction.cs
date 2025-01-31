using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("PowerAndFunction")]
    public partial class PowerAndFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PowerAndFunction()
        {
            PowerAndFunctionMakerCheckers = new HashSet<PowerAndFunctionMakerChecker>();
            PowerAndFunctionTranslations = new HashSet<PowerAndFunctionTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid PowerAndFunctionId { get; set; }

        [StringLength(3)]
        public String PowerAndFunctionFor { get; set; }

        [Required]
        [StringLength(4000)]
        public string NameOfPowerAndFunction { get; set; }

        [Required]
        [StringLength(150)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(1000)]
        public string NameOnReport { get; set; }

        public int ParentPrmKey { get; set; }

        public bool IsTitle { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PowerAndFunctionMakerChecker> PowerAndFunctionMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PowerAndFunctionTranslation> PowerAndFunctionTranslations { get; set; }
    }
}
