using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.SystemEntity
{
    [Table("ChecksumAlgorithm")]
    public partial class ChecksumAlgorithm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid ChecksumAlgorithmId { get; set; }

        [Required]
        [StringLength(100)]
        public string SysNameOfChecksumAlgorithm { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfChecksumAlgorithm { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
