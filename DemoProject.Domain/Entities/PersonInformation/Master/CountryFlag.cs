using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CountryFlag")]
    public partial class CountryFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CountryFlag()
        {
            CountryFlagMakerCheckers = new HashSet<CountryFlagMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public Guid CountryFlagId { get; set; }

        public short CenterPrmKey { get; set; }

        [Required]
        public byte[] FlagImg { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CountryFlagMakerChecker> CountryFlagMakerCheckers { get; set; }
    }
}
