using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{

    [Table("PersonDeath")]
    public partial class PersonDeath
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonDeath()
        {
            PersonDeathMakerCheckers = new HashSet<PersonDeathMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonDeathId { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime DeathDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonDeathMakerChecker> PersonDeathMakerCheckers { get; set; }
    }
}
