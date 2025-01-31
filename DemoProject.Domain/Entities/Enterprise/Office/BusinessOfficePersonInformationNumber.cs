using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficePersonInformationNumber")]
    public partial class BusinessOfficePersonInformationNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficePersonInformationNumber()
        {
            BusinessOfficePersonInformationNumberMakerCheckers = new HashSet<BusinessOfficePersonInformationNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoPersonInformationNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string PersonInformationNumberMask { get; set; }

        public int StartPersonInformationNumber { get; set; }

        public int EndPersonInformationNumber { get; set; }

        public int PersonInformationNumberIncrementBy { get; set; }

        public bool EnableRandomPersonInformationNumber { get; set; }

        public bool EnableCustomizePersonInformationNumber { get; set; }

        public bool EnableReGenerateUnusedPersonInformationNumber { get; set; }

        public bool EnableDigitalCodeForPersonInformationNumber { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficePersonInformationNumberMakerChecker> BusinessOfficePersonInformationNumberMakerCheckers { get; set; }
    }
}
