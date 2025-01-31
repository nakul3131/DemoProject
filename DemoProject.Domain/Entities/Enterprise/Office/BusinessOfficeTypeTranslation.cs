using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeTypeTranslation")]
    public partial class BusinessOfficeTypeTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeTypeTranslation()
        {
            BusinessOfficeTypeTranslationMakerCheckers = new HashSet<BusinessOfficeTypeTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficeTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfBusinessOfficeType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual BusinessOfficeType BusinessOfficeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTypeTranslationMakerChecker> BusinessOfficeTypeTranslationMakerCheckers { get; set; }
    }
}
