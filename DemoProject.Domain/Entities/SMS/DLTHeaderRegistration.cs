using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("DLTHeaderRegistration")]
    public partial class DLTHeaderRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DLTHeaderRegistration()
        {
            DLTHeaderRegistrationMakerCheckers = new HashSet<DLTHeaderRegistrationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        [Required]
        public Guid DLTHeaderRegistrationId { get; set; }

        [Required]
        [StringLength(50)]
        public string HeaderId { get; set; }

        [Required]
        [StringLength(6)]
        public string Header { get; set; }

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
        public virtual ICollection<DLTHeaderRegistrationMakerChecker> DLTHeaderRegistrationMakerCheckers { get; set; }
    }
}
