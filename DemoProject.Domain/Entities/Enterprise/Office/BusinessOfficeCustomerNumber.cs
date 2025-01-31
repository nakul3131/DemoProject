using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeCustomerNumber")]
    public partial class BusinessOfficeCustomerNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeCustomerNumber()
        {
            BusinessOfficeCustomerNumberMakerCheckers = new HashSet<BusinessOfficeCustomerNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

     //   public Guid BusinessOfficeCustomerNumberId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int StartCustomerNumber { get; set; }

        public int EndCustomerNumber { get; set; }

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
        public virtual ICollection<BusinessOfficeCustomerNumberMakerChecker> BusinessOfficeCustomerNumberMakerCheckers { get; set; }
    }
}
