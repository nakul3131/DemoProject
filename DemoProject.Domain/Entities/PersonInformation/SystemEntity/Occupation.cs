using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Domain.Entities.PersonInformation.Master;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("Occupation")]
    public partial class Occupation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Occupation()
        {            
            OccupationTranslations = new HashSet<OccupationTranslation>();
            CenterOccupations = new HashSet<CenterOccupation>();
            PersonAdditionalDetails = new HashSet<PersonAdditionalDetail>();
            CustomerLoanAccounts = new HashSet<CustomerLoanAccount>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid OccupationId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfOccupation { get; set; }

        [Required]
        [StringLength(10)]
        public string SysNameOfOccupation { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short ParentOccupationPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OccupationTranslation> OccupationTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterOccupation> CenterOccupations { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalDetail> PersonAdditionalDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccount> CustomerLoanAccounts { get; set; }

    }
}
