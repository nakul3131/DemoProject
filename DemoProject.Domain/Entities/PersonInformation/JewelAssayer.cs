using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("JewelAssayer")]
    public partial class JewelAssayer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JewelAssayer()
        {
            JewelAssayerMakerCheckers = new HashSet<JewelAssayerMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid JewelAssayerId { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JewelAssayerMakerChecker> JewelAssayerMakerCheckers { get; set; }
    }
}
