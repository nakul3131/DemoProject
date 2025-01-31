using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("ConsumerDurableItemModification")]
    public partial class ConsumerDurableItemModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConsumerDurableItemModification()
        {
            ConsumerDurableItemModificationMakerCheckers = new HashSet<ConsumerDurableItemModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfItem { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public string ReasonForModification { get; set; }


        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual ConsumerDurableItem ConsumerDurableItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumerDurableItemModificationMakerChecker> ConsumerDurableItemModificationMakerCheckers { get; set; }
    }
}
