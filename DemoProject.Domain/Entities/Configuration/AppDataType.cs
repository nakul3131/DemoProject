using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("AppDataType")]
    public partial class AppDataType
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid DataTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfDataType { get; set; }

        [Required]
        [StringLength(3)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        public DateTime ActivateDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(3000)]
        public string Note { get; set; }
    }
}
