using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CenterOccupation")]
    public partial class CenterOccupation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterOccupation()
        {
            CenterOccupationMakerCheckers = new HashSet<CenterOccupationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterOccupationId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short OccupationPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        public virtual Occupation Occupation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterOccupationMakerChecker> CenterOccupationMakerCheckers { get; set; }
    }
}
