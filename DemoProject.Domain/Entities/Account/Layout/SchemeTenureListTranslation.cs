using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTenureListTranslation")]
    public partial class SchemeTenureListTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTenureListTranslation()
        {
            SchemeTenureListTranslationMakerCheckers = new HashSet<SchemeTenureListTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemeTenureListPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransTenureText { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SchemeTenureList SchemeTenureList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenureListTranslationMakerChecker> SchemeTenureListTranslationMakerCheckers { get; set; }
    }
}
