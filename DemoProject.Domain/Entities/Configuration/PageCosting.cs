using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageCosting")]
    public partial class PageCosting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PageCosting()
        {
            PageAddOnSubscriptions = new HashSet<PageAddOnSubscription>();
        }

        [Key]
        public short PrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short PagePrmKey { get; set; }

        public decimal MinimumCostAmount { get; set; }

        public decimal MaximumCostAmount { get; set; }

        public decimal StandardCostAmount { get; set; }

        public short ValidityInDays { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public virtual Page Page { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageAddOnSubscription> PageAddOnSubscriptions { get; set; }
    }
}
