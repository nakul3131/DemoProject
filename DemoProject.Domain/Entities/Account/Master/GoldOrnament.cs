using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Customer;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("GoldOrnament")]
    public partial class GoldOrnament
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldOrnament()
        {
            CustomerGoldLoanCollateralDetails = new HashSet<CustomerGoldLoanCollateralDetail>();
            GoldOrnamentMakerCheckers = new HashSet<GoldOrnamentMakerChecker>();
            GoldOrnamentModifications = new HashSet<GoldOrnamentModification>();
            GoldOrnamentTranslations = new HashSet<GoldOrnamentTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid GoldOrnamentId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfGoldOrnament { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

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
        public virtual ICollection<CustomerGoldLoanCollateralDetail> CustomerGoldLoanCollateralDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldOrnamentMakerChecker> GoldOrnamentMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldOrnamentModification> GoldOrnamentModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldOrnamentTranslation> GoldOrnamentTranslations { get; set; }
    }
}
