using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationContactDetail")]
    public partial class OrganizationContactDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationContactDetail()
        {
            OrganizationContactDetailMakerCheckers = new HashSet<OrganizationContactDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid OrganizationContactDetailId { get; set; }

        public byte ContactTypePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string FieldValue { get; set; }

        public byte ContactGroupPrmKey { get; set; }

        public bool IsOpen { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationContactDetailMakerChecker> OrganizationContactDetailMakerCheckers { get; set; }
    }
}
