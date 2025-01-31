using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("EvaluationSectorContentItem")]
    public partial class EvaluationSectorContentItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluationSectorContentItem()
        {
            EvaluationSectorContentItemMakerCheckers = new HashSet<EvaluationSectorContentItemMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid EvaluationSectorContentItemId { get; set; }

        public short EvaluationSectionPrmKey { get; set; }

        public short ContentItemPrmKey { get; set; }

        public short SequenceNumber { get; set; }

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

        public virtual ContentItem ContentItem { get; set; }

        public virtual EvaluationSection EvaluationSection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationSectorContentItemMakerChecker> EvaluationSectorContentItemMakerCheckers { get; set; }
    }
}
