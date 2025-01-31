using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("ConsumerDurableItemTranslation")]
    public partial class ConsumerDurableItemTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConsumerDurableItemTranslation()
        {
            ConsumerDurableItemTranslationMakerCheckers = new HashSet<ConsumerDurableItemTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfItem { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual ConsumerDurableItem ConsumerDurableItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumerDurableItemTranslationMakerChecker> ConsumerDurableItemTranslationMakerCheckers { get; set; }
    }
}
