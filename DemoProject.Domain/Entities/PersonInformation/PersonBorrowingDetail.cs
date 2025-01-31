using DemoProject.Domain.Entities.Account.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonBorrowingDetail")]
    public partial class PersonBorrowingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonBorrowingDetail()
        {
            PersonBorrowingDetailMakerCheckers = new HashSet<PersonBorrowingDetailMakerChecker>();
            PersonBorrowingDetailTranslations = new HashSet<PersonBorrowingDetailTranslation>();
            CustomerLoanAccountBorrowingDetails = new HashSet<CustomerLoanAccountBorrowingDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonBorrowingDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string Branch { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime MatureDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string LoanDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string MortgageDetails { get; set; }

        public decimal MortgageAmount { get; set; }

        public decimal SanctionLoanAmount { get; set; }

        public decimal InstallmentAmount { get; set; }

        public decimal LoanBalanceAmount { get; set; }
        
        public short OverduesInstallment { get; set; }

        public decimal OverduesAmount { get; set; }

        public bool IsTakingAnyCourtAction { get; set; }

        public byte CourtCaseTypePrmKey { get; set; }

        public DateTime FilingDate { get; set; }

        [Required]
        [StringLength(50)]
        public string FilingNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CNRNumber { get; set; }

        public byte CourtCaseStagePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBorrowingDetailMakerChecker> PersonBorrowingDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBorrowingDetailTranslation> PersonBorrowingDetailTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountBorrowingDetail> CustomerLoanAccountBorrowingDetails { get; set; }
    }
}
