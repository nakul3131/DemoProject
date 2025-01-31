using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesApplicationNumberMask")]
    public partial class SharesApplicationNumberMask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public Guid SharesApplicationNumberMaskId { get; set; }

        public short SharesApplicationParameterPrmKey { get; set; }

        public byte SharesApplicationMaskTypePrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
