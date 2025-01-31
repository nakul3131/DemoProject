using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CountryAdditionalDetail")]
    public partial class CountryAdditionalDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CountryAdditionalDetail()
        {
            CountryAdditionalDetailMakerCheckers = new HashSet<CountryAdditionalDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CountryAdditionalDetailId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte MinorAge { get; set; }

        public short WorldWideTimeZonePrmKey { get; set; }

        public short InternationalDialingCodes { get; set; }

        public short CurrencyPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CountryAdditionalDetailMakerChecker> CountryAdditionalDetailMakerCheckers { get; set; }
    }
}
