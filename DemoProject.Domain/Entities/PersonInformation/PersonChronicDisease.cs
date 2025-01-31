using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonChronicDisease")]
    public partial class PersonChronicDisease
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonChronicDisease()
        {
            PersonChronicDiseaseMakerCheckers = new HashSet<PersonChronicDiseaseMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonChronicDiseaseId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DiseasePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string OtherDetails { get; set; }

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
        public virtual ICollection<PersonChronicDiseaseMakerChecker> PersonChronicDiseaseMakerCheckers { get; set; }

    }
}
