using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("AuthorizedSharesCapital")]
    public partial class AuthorizedSharesCapital
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthorizedSharesCapital()
        {
            AuthorizedSharesCapitalMakerCheckers = new HashSet<AuthorizedSharesCapitalMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid AuthorizedSharesCapitalId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime AuthorizedDate { get; set; }

        public decimal AuthorizedSharesCapitalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        [Required]
        [StringLength(4000)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorizedSharesCapitalMakerChecker> AuthorizedSharesCapitalMakerCheckers { get; set; }
    }
}
