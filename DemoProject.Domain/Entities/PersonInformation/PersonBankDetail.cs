using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonBankDetail")]
    public partial class PersonBankDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonBankDetail()
        {
            PersonBankDetailDocuments = new HashSet<PersonBankDetailDocument>();
            PersonBankDetailMakerCheckers = new HashSet<PersonBankDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BankBranchPrmKey { get; set; }

        public long AccountNumber { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public bool IsDefaultBankForTransaction { get; set; }

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
        public virtual ICollection<PersonBankDetailMakerChecker> PersonBankDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBankDetailDocument> PersonBankDetailDocuments { get; set; }
    }
}
