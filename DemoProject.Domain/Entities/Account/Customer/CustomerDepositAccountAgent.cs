using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerDepositAccountAgent")]
    public partial class CustomerDepositAccountAgent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerDepositAccountAgent()
        {
            CustomerDepositAccountAgentMakerCheckers = new HashSet<CustomerDepositAccountAgentMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int AgentPrmKey { get; set; }

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

        public virtual CustomerDepositAccount CustomerDepositAccount { get; set; }

        //public virtual Agent Agent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDepositAccountAgentMakerChecker> CustomerDepositAccountAgentMakerCheckers { get; set; }
    }
}
