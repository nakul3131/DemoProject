using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("ConsumerDurableItemBrand")]
    public partial class ConsumerDurableItemBrand
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid ConsumerDurableItemBrandId { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfBrand { get; set; }

        public short EstablishedYear { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

    }
}
