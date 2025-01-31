using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("DividendCalculationMethod")]
    public partial class DividendCalculationMethod
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid DividendCalculationMethodId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfMethod { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(100)]
        public string Formula { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
