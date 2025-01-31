using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Parameter
{
    [Table("BoardOfDirectorParameter")]
    public partial class BoardOfDirectorParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardOfDirectorParameter()
        {
            BoardOfDirectorParameterMakerCheckers = new HashSet<BoardOfDirectorParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BoardOfDirectorParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte TotalNumberOfDirectors { get; set; }

        public byte BoardOfDirectorValidity { get; set; }

        public byte ReserveSeatsForGeneralCategory { get; set; }

        public byte ReserveSeatsForSCST { get; set; }

        public byte ReserveSeatsForWeakerSection { get; set; }

        public byte ReserveSeatsForOBC { get; set; }

        public byte ReserveSeatsForWomen { get; set; }

        public byte ReserveSeatsForDNotifiedTribes { get; set; }

        public byte ReserveSeatsForCooptExpertDirector { get; set; }

        public bool IsRequiredActiveMemberForDirector { get; set; }

        public byte AssuranceDeedFormat { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DirectorAndTheirRelativesMortgageLoanLimitAgainstTotalLoanInPercentage { get; set; }

        public decimal DirectorAndTheirRelativesMortgageLoanLimitAgainstTotalLoanAmount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardOfDirectorParameterMakerChecker> BoardOfDirectorParameterMakerCheckers { get; set; }
    }
}
