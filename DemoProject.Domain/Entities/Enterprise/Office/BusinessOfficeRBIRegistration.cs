using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeRBIRegistration")]
    public partial class BusinessOfficeRBIRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeRBIRegistration()
        {
            BusinessOfficeRBIRegistrationMakerCheckers = new HashSet<BusinessOfficeRBIRegistrationMakerChecker>();
            BusinessOfficeRBIRegistrationTranslations = new HashSet<BusinessOfficeRBIRegistrationTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

       // public Guid BusinessOfficeRBIRegistrationId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime ApprovalDate { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        public short UniformBusinessOfficeCode1 { get; set; }

        public short UniformBusinessOfficeCode2 { get; set; }

        public short BusinessOfficeCodeByRBI { get; set; }

        public short MICRCode { get; set; }

        [Required]
        [StringLength(11)]
        public string IFSCCode { get; set; }

        [Required]
        [StringLength(50)]
        public string AlphaNumericSWIFTAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string AlphaNumericTelexAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessOfficeUniqueIdentityNumberForATM { get; set; }

        [Required]
        [StringLength(20)]
        public string RoutingNumberForClearingTransaction { get; set; }

        [Required]
        [StringLength(2)]
        public string IBANCountryCode { get; set; }

        public byte IBANCheckDigitAlgorithm { get; set; }

        [Required]
        [StringLength(30)]
        public string BBANFormatMask { get; set; }

        [Required]
        [StringLength(1)]
        public string BBANDataType { get; set; }

        public byte BBANCheckDigitAlgorithm { get; set; }

        public byte UserDefinedAlgorithm { get; set; }

        [Required]
        [StringLength(30)]
        public string BBANBankCode { get; set; }

        [Required]
        [StringLength(30)]
        public string BBANBranchCode { get; set; }

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
        public virtual ICollection<BusinessOfficeRBIRegistrationMakerChecker> BusinessOfficeRBIRegistrationMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeRBIRegistrationTranslation> BusinessOfficeRBIRegistrationTranslations { get; set; }
    }
}
