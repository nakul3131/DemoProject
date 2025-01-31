using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("Page")]
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            PageCostings = new HashSet<PageCosting>();
            PageTables = new HashSet<PageTable>();
            PageTranslations = new HashSet<PageTranslation>();
        }

        [Key]

        public short PrmKey { get; set; }

        public int MenuPrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPage { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfActionMethod { get; set; }

        [Required]
        [StringLength(1)]
        public string TypeOfPage { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForClose { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageCosting> PageCostings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageTable> PageTables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageTranslation> PageTranslations { get; set; }
    }
}
