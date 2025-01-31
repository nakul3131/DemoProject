using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Servant
{
    [Table("EmployeeDetail")]
    public partial class EmployeeDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeDetail()
        {
            EmployeeDetailMakerCheckers = new HashSet<EmployeeDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte EmployeeTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime DateOfJoining { get; set; }

        public DateTime? DateOfLeaving { get; set; }

        public short PostingPlacePrmKey { get; set; }

        public DateTime DateOfProbation { get; set; }

        public DateTime DateOfConfirmation { get; set; }

        public DateTime DateOfTrainingStarted { get; set; }

        public DateTime DateOfTrainingEnded { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Employee Employee { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDetailMakerChecker> EmployeeDetailMakerCheckers { get; set; }
    }
}
