using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficePassbookNumber")]
    public partial class BusinessOfficePassbookNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficePassbookNumber()
        {
            BusinessOfficePassbookNumberMakerCheckers = new HashSet<BusinessOfficePassbookNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoPassbookNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string PassbookNumberMask { get; set; }

        public int StartPassbookNumber { get; set; }

        public int EndPassbookNumber { get; set; }

        public int PassbookNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedPassbookNumber { get; set; }

        public bool EnableRandomPassbookNumber { get; set; }

        public bool EnableCustomizePassbookNumber { get; set; }

        public bool EnableDigitalCodeForPassbookNumber { get; set; }

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
        public virtual ICollection<BusinessOfficePassbookNumberMakerChecker> BusinessOfficePassbookNumberMakerCheckers { get; set; }
    }
}
