using DemoProject.Domain.Entities.Account.GL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeApplicationNumber")]
    public partial class BusinessOfficeApplicationNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeApplicationNumber()
        {
            BusinessOfficeApplicationNumberMakerCheckers = new HashSet<BusinessOfficeApplicationNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoApplicationNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string ApplicationNumberMask { get; set; }

        public int StartApplicationNumber { get; set; }

        public int EndApplicationNumber { get; set; }

        public int ApplicationNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedApplicationNumber { get; set; }

        public bool EnableRandomApplicationNumber { get; set; }

        public bool EnableCustomizeApplicationNumber { get; set; }

        public bool EnableDigitalCodeForApplicationNumber { get; set; }

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
        public virtual ICollection<BusinessOfficeApplicationNumberMakerChecker> BusinessOfficeApplicationNumberMakerCheckers { get; set; }
    }
}
