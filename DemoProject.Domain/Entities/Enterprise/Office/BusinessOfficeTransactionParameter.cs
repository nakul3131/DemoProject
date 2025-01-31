using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{

    [Table("BusinessOfficeTransactionParameter")]
    public partial class BusinessOfficeTransactionParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeTransactionParameter()
        {
            BusinessOfficeTransactionParameterMakerCheckers = new HashSet<BusinessOfficeTransactionParameterMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

      //  public Guid BusinessOfficeTransactionParameterId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        [Required]
        [StringLength(20)]
        public string TransactionNumberMask { get; set; }

        public byte ChecksumAlgorithmPrmKey { get; set; }

        public int StartTransactionNumber { get; set; }

        public int EndTransactionNumber { get; set; }

        public int TransactionNumberIncrementBy { get; set; }

        [StringLength(3)]
        public string TransactionNumberReset { get; set; }

        public bool EnableAutoGenerateTransactionNumber { get; set; }

        public bool EnableRegenerateUnusedTransactionNumber { get; set; }

        public bool EnableTransactionDigitalCode { get; set; }

    //    public short FrequencyPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTransactionParameterMakerChecker> BusinessOfficeTransactionParameterMakerCheckers { get; set; }
    }
}
