using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("PaymentCard")]
    public partial class PaymentCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentCard()
        {
            PaymentCardTranslations = new HashSet<PaymentCardTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid PaymentCardId { get; set; }

        public byte CardTypePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPaymentCard { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public byte CardPaymentProcessorCompanyPrmKey { get; set; }

        public bool EnableDomesticATM { get; set; }

        public bool EnableDomesticATMCashWithdrawl { get; set; }

        public bool EnableDomesticATMCashWithdrawlLimit { get; set; }

        public bool EnableDomesticFundTransfer { get; set; }

        public bool EnableDomesticFundTransferLimit { get; set; }

        public bool EnableInternationalATM { get; set; }

        public bool EnableInternationalATMCashWithdrawl { get; set; }

        public bool EnableInternationalATMCashWithdrawlLimit { get; set; }

        public bool EnableInternationalFundTransfer { get; set; }

        public bool EnableInternationalFundTransferLimit { get; set; }

        public bool EnableDomesticPOSTerminal { get; set; }

        public bool EnableDomesticPOSTerminalSwipe { get; set; }

        public bool EnableDomesticPOSTerminalDipped { get; set; }

        public bool EnableDomesticPOSTerminalTapped { get; set; }

        public bool EnableDomesticPOSTerminalLimit { get; set; }

        public bool EnableInternationalPOSTerminal { get; set; }

        public bool EnableInternationalPOSTerminalSwipe { get; set; }

        public bool EnableInternationalPOSTerminalDipped { get; set; }

        public bool EnableInternationalPOSTerminalTapped { get; set; }

        public bool EnableInternationalPOSTerminalLimit { get; set; }

        public bool EnableDomesticECommerce { get; set; }

        public bool EnableDomesticECommerceLimit { get; set; }

        public bool EnableInternationalECommerce { get; set; }

        public bool EnableInternationalECommerceLimit { get; set; }

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
        public virtual ICollection<PaymentCardTranslation> PaymentCardTranslations { get; set; }
    }
}
