using DemoProject.Domain.Entities.Account.Customer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAdditionalIncomeDetail")]
    public partial class PersonAdditionalIncomeDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAdditionalIncomeDetail()
        {
            PersonAdditionalIncomeDetailMakerCheckers = new HashSet<PersonAdditionalIncomeDetailMakerChecker>();
            CustomerLoanAccountAdditionalIncomeDetails = new HashSet<CustomerLoanAccountAdditionalIncomeDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAdditionalIncomeDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short IncomeSourcePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string OtherDetails { get; set; }

        public decimal AnnualIncome { get; set; }

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
        public virtual ICollection<PersonAdditionalIncomeDetailMakerChecker> PersonAdditionalIncomeDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountAdditionalIncomeDetail> CustomerLoanAccountAdditionalIncomeDetails { get; set; }
    }
}
