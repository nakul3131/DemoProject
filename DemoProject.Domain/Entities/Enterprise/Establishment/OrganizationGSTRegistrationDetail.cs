using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationGSTRegistrationDetail")]
    public partial class OrganizationGSTRegistrationDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationGSTRegistrationDetail()
        {
            OrganizationGSTRegistrationDetailMakerCheckers = new HashSet<OrganizationGSTRegistrationDetailMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid OrganizationGSTRegistrationDetailId { get; set; }

        public short StatePrmKey { get; set; }

        public short GSTRegistrationTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public byte GSTReturnPeriodicityPrmKey { get; set; }

        public bool IsApplicableEWayBill { get; set; }

        public decimal ThresholdLimit { get; set; }

        [Required]
        [StringLength(15)]
        public string GSTRegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationGSTRegistrationDetailMakerChecker> OrganizationGSTRegistrationDetailMakerCheckers { get; set; }
    }
}
