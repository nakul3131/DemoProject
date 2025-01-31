using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonCreditRating")]
    public partial class PersonCreditRating
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonCreditRating()
        {
            PersonCreditRatingMakerCheckers = new HashSet<PersonCreditRatingMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonCreditRatingId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte CreditBureauAgencyPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short Score { get; set; }

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

        public virtual CreditBureauAgency CreditBureauAgency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCreditRatingMakerChecker> PersonCreditRatingMakerCheckers { get; set; }
    }

}
