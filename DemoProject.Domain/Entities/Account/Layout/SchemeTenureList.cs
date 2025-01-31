using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTenureList")]
    public partial class SchemeTenureList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTenureList()
        {
            SchemeTenureListMakerCheckers = new HashSet<SchemeTenureListMakerChecker>();
            SchemeTenureListTranslations = new HashSet<SchemeTenureListTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public Guid SchemeTenureListId { get; set; }

        public short SchemePrmKey { get; set; }

        public short Tenure { get; set; }

        [Required]
        [StringLength(1)]
        public string TenureUnit { get; set; }

        [Required]
        [StringLength(100)]
        public string TenureText { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenureListMakerChecker> SchemeTenureListMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenureListTranslation> SchemeTenureListTranslations { get; set; }
    }
}
