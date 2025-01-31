using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonStatus")]
    public partial class PersonStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonStatus()
        {
            PersonStatusMakerCheckers = new HashSet<PersonStatusMakerChecker>();
        }

        [Key]
        public byte PrmKey { get; set; }

        //public Guid PersonStatusId { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte MemberTypePrmKey { get; set; }

        public bool IsDepositor { get; set; }

        public byte BorrowingStatus { get; set; }

        public byte GuarantorStatus { get; set; }

        public byte GuarantorForNumberOfLoans { get; set; }

        public bool IsActiveMember { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonStatusMakerChecker> PersonStatusMakerCheckers { get; set; }
    }
}
