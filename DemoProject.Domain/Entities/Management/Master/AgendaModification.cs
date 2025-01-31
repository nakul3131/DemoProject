using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("AgendaModification")]
    public partial class AgendaModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgendaModification()
        {
            AgendaModificationMakerCheckers = new HashSet<AgendaModificationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int AgendaPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(4000)]
        public string NameOfAgenda { get; set; }

        [Required]
        [StringLength(100)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(4000)]
        public string NameOnReport { get; set; }

        public TimeSpan TimeAllocation { get; set; }

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

        public virtual Agenda Agenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaModificationMakerChecker> AgendaModificationMakerCheckers { get; set; }
    }
}
