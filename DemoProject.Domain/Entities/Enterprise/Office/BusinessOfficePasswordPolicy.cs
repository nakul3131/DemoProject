using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficePasswordPolicy")]
    public partial class BusinessOfficePasswordPolicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficePasswordPolicy()
        {
            BusinessOfficePasswordPolicyMakerCheckers = new HashSet<BusinessOfficePasswordPolicyMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

     //   public Guid BusinessOfficePasswordPolicyId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short PasswordPolicyPrmKey { get; set; }

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
        public virtual ICollection<BusinessOfficePasswordPolicyMakerChecker> BusinessOfficePasswordPolicyMakerCheckers { get; set; }
    }
}
