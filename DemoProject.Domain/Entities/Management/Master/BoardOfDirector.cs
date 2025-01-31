using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{

    [Table("BoardOfDirector")]
    public partial class BoardOfDirector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardOfDirector()
        {
            BoardOfDirectorMakerCheckers = new HashSet<BoardOfDirectorMakerChecker>();
            BoardOfDirectorPowerAndFunctions = new HashSet<BoardOfDirectorPowerAndFunction>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short DesignationPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string SequenceNumberText { get; set; }

        public DateTime DateOfAppointment { get; set; }

        public DateTime? DateOfTermination { get; set; }

        public bool IsDisqualified { get; set; }

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
        public virtual ICollection<BoardOfDirectorMakerChecker> BoardOfDirectorMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardOfDirectorPowerAndFunction> BoardOfDirectorPowerAndFunctions { get; set; }
    }
}
