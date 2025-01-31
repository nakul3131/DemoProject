using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("BoardOfDirectorPowerAndFunction")]
    public partial class BoardOfDirectorPowerAndFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardOfDirectorPowerAndFunction()
        {
            BoardOfDirectorPowerAndFunctionMakerCheckers = new HashSet<BoardOfDirectorPowerAndFunctionMakerChecker>();
            BoardOfDirectorPowerAndFunctionTranslations = new HashSet<BoardOfDirectorPowerAndFunctionTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid BoardOfDirectorPowerAndFunctionId { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        public int PowerAndFunctionPrmKey { get; set; }

        public short SequenceNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string SequenceText { get; set; }

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

        public virtual BoardOfDirector BoardOfDirector { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardOfDirectorPowerAndFunctionMakerChecker> BoardOfDirectorPowerAndFunctionMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardOfDirectorPowerAndFunctionTranslation> BoardOfDirectorPowerAndFunctionTranslations { get; set; }
    }
}
