using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("BeneficiaryDetail")]
    public partial class BeneficiaryDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BeneficiaryDetail()
        {
            BeneficiaryDetailMakerCheckers = new HashSet<BeneficiaryDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid BeneficiaryDetailId { get; set; }

        public DateTime EffectiveDate { get; set; }

        [Required]
        [StringLength(50)]
        public string BeneficiaryCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBeneficiary { get; set; }

        [Required]
        [StringLength(20)]
        public string ShortName { get; set; }

        public short CustomerAccountTypePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string IfscCode { get; set; }

        [Required]
        [StringLength(10)]
        public string BankName { get; set; }

        [Required]
        [StringLength(50)]
        public string Branch { get; set; }

        [Required]
        [StringLength(10)]
        public string City { get; set; }

        public int CustomerNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string EmailId { get; set; }

        [Required]
        [StringLength(250)]
        public string VirtualPrivateAddress { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeneficiaryDetailMakerChecker> BeneficiaryDetailMakerCheckers { get; set; }
    }
}
