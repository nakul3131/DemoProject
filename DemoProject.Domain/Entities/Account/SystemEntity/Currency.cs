using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Enterprise.Office;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("Currency")]
    public partial class Currency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Currency()
        {
            BusinessOfficeCurrencies = new HashSet<BusinessOfficeCurrency>();
            BusinessOfficeDetails = new HashSet<BusinessOfficeDetail>();
            BusinessOfficeTransactionLimits = new HashSet<BusinessOfficeTransactionLimit>();
            CurrencyTranslations = new HashSet<CurrencyTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CurrencyId { get; set; }

        [Required]
        [StringLength(20)]
        public string CurrencyCode { get; set; }

        [Required]
        [StringLength(3)]
        public string ISOAlphabeticCurrencyCode { get; set; }

        public short ISONumericCurrencyCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCurrency { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyType { get; set; }

        public byte[] CurrencySymbol { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCurrency> BusinessOfficeCurrencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDetail> BusinessOfficeDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTransactionLimit> BusinessOfficeTransactionLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyTranslation> CurrencyTranslations { get; set; }

    }
}
