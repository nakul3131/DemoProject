using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Enterprise.Office;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("Language")]
    public partial class Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Language()
        {
            BusinessOfficeDetails = new HashSet<BusinessOfficeDetail>();
        }

        [Key]

        public short PrmKey { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        public string NameOfLanguage { get; set; }

        [Required]
        [StringLength(20)]
        public string NameInUnicode { get; set; }

        [Required]
        [StringLength(20)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(400)]
        public string Months { get; set; }

        [Required]
        [StringLength(150)]
        public string ShortMonths { get; set; }

        [Required]
        [StringLength(250)]
        public string WeekDays { get; set; }

        [Required]
        [StringLength(250)]
        public string ShortWeekDays { get; set; }

        [Required]
        [StringLength(100)]
        public string Numbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDetail> BusinessOfficeDetails { get; set; }
    }
}
