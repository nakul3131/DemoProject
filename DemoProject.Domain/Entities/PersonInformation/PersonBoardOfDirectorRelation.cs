using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonBoardOfDirectorRelation")]
    public partial class PersonBoardOfDirectorRelation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonBoardOfDirectorRelation()
        {
            PersonBoardOfDirectorRelationMakerCheckers = new HashSet<PersonBoardOfDirectorRelationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        public byte RelationPrmKey { get; set; }

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
        public virtual ICollection<PersonBoardOfDirectorRelationMakerChecker> PersonBoardOfDirectorRelationMakerCheckers { get; set; }
    }
}
