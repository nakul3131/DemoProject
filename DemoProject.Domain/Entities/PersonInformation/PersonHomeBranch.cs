using DemoProject.Domain.Entities.Enterprise.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonHomeBranch")]
    public partial class PersonHomeBranch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonHomeBranch()
        {
            PersonHomeBranchMakerCheckers = new HashSet<PersonHomeBranchMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonHomeBranchId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short PersonRegionalLanguagePrmKey { get; set; }

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

        public virtual Person Person { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonHomeBranchMakerChecker> PersonHomeBranchMakerCheckers { get; set; }
    }
}
