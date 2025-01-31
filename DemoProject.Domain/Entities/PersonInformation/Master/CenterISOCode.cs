using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CenterISOCode")]
    public partial class CenterISOCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterISOCode()
        {
            CenterISOCodeMakerCheckers = new HashSet<CenterISOCodeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterISOCodeId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(2)]
        public string ISOAlphaNumericCode2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ISOAlphaNumericCode3 { get; set; }

        public short ISONumericCode { get; set; }

        [Required]
        [StringLength(20)]
        public string OtherCode { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterISOCodeMakerChecker> CenterISOCodeMakerCheckers { get; set; }
    }
}
