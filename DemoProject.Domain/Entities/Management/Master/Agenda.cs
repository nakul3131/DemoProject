using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.Entities.Management.Master
{
    public partial class Agenda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agenda()
        {
            AgendaMakerCheckers = new HashSet<AgendaMakerChecker>();
            AgendaModifications = new HashSet<AgendaModification>();
            AgendaTranslations = new HashSet<AgendaTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid AgendaId { get; set; }

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
        public virtual ICollection<AgendaMakerChecker> AgendaMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaModification> AgendaModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTranslation> AgendaTranslations { get; set; }
    }
}
