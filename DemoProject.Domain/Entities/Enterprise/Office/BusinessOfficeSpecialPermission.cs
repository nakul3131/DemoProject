using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeSpecialPermission")]
    public partial class BusinessOfficeSpecialPermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeSpecialPermission()
        {
            BusinessOfficeSpecialPermissionMakerCheckers = new HashSet<BusinessOfficeSpecialPermissionMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

      //  public Guid BusinessOfficeSpecialPermissionId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short SpecialPermissionPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeSpecialPermissionMakerChecker> BusinessOfficeSpecialPermissionMakerCheckers { get; set; }
    }
}
