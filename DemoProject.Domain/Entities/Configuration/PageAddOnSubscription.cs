using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageAddOnSubscription")]
    public partial class PageAddOnSubscription
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EffectiveDateTime { get; set; }

        public short AddOnSubscriptionPrmKey { get; set; }

        public short PageCostingPrmKey { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public virtual PageCosting PageCosting { get; set; }
    }
}
