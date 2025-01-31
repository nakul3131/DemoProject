using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("ConsumerDurableItem")]
    public partial class ConsumerDurableItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConsumerDurableItem()
        {
            ConsumerDurableItemMakerCheckers = new HashSet<ConsumerDurableItemMakerChecker>();
            ConsumerDurableItemModifications = new HashSet<ConsumerDurableItemModification>();
            ConsumerDurableItemTranslations = new HashSet<ConsumerDurableItemTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid ConsumerDurableItemId { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfItem { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public bool IsModified { get; set; }
        

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumerDurableItemMakerChecker> ConsumerDurableItemMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumerDurableItemModification> ConsumerDurableItemModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumerDurableItemTranslation> ConsumerDurableItemTranslations { get; set; }
    }
}
