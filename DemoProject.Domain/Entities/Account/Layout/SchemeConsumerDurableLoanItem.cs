using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeConsumerDurableLoanItem")]
    public partial class SchemeConsumerDurableLoanItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeConsumerDurableLoanItem()
        {
            SchemeConsumerDurableLoanItemMakerCheckers = new HashSet<SchemeConsumerDurableLoanItemMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public decimal Margin { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeConsumerDurableLoanItemMakerChecker> SchemeConsumerDurableLoanItemMakerCheckers { get; set; }
    }
}
