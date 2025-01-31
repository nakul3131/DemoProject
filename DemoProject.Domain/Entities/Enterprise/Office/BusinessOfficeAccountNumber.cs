using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeAccountNumber")]
    public partial class BusinessOfficeAccountNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeAccountNumber()
        {
            BusinessOfficeAccountNumberMakerCheckers = new HashSet<BusinessOfficeAccountNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAccountNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string AccountNumberMask { get; set; }

        public long StartAccountNumber { get; set; }

        public long EndAccountNumber { get; set; }

        public int AccountNumberIncrementBy { get; set; }

        public bool EnableRandomAccountNumber { get; set; }

        public bool EnableCustomizeAccountNumber { get; set; }

        public bool EnableReGenerateUnusedAccountNumber { get; set; }

        public bool EnableDigitalCodeForAccountNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeAccountNumberMakerChecker> BusinessOfficeAccountNumberMakerCheckers { get; set; }
    }
}
