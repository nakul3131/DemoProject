using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("DLTRegistration")]
    public partial class DLTRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DLTRegistration()
        {
            DLTRegistrationMakerCheckers = new HashSet<DLTRegistrationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        [Required]
        public Guid DLTRegistrationId { get; set; }

        public byte DLTPortalPrmKey { get; set;}

        [Required]
        [StringLength(1500)]
        public string NameOfPrincipalEntity { get; set; }

        [Required]
        [StringLength(50)]
        public string PrincipalEntityId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(1500)]
        public string AuthorizedPersonName { get; set; }

        [Required]
        [StringLength(10)]
        public string RegisteredMobileNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string RegisteredEmailId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DLTRegistrationMakerChecker> DLTRegistrationMakerCheckers { get; set; }
    }
}
