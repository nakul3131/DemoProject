using DemoProject.Domain.Entities.Account.SystemEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeCurrency")]
    public partial class BusinessOfficeCurrency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeCurrency()
        {
            BusinessOfficeCurrencyMakerCheckers = new HashSet<BusinessOfficeCurrencyMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

       // public Guid BusinessOfficeCurrencyId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

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

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCurrencyMakerChecker> BusinessOfficeCurrencyMakerCheckers { get; set; }
    }
}
