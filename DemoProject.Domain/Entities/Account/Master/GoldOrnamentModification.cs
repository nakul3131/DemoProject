using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("GoldOrnamentModification")]
    public partial class GoldOrnamentModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldOrnamentModification()
        {
            GoldOrnamentModificationMakerCheckers = new HashSet<GoldOrnamentModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short GoldOrnamentPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfGoldOrnament { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual GoldOrnament GoldOrnament { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldOrnamentModificationMakerChecker> GoldOrnamentModificationMakerCheckers { get; set; }
    }
}
