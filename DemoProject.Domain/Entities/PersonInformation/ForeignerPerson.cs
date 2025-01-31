using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("ForeignerPerson")]
    public partial class ForeignerPerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForeignerPerson()
        {
            ForeignerPersonMakerCheckers = new HashSet<ForeignerPersonMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

      //  public Guid ForeignerPersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool IsPermanentThisCountryResidentStatus { get; set; }

        public bool IsVisitedThisCountryInLast3Years { get; set; }

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
        public virtual ICollection<ForeignerPersonMakerChecker> ForeignerPersonMakerCheckers { get; set; }
    }
}
