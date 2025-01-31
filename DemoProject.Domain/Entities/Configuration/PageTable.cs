using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageTable")]
    public partial class PageTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PageTable()
        {
            PageTableFields = new HashSet<PageTableField>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short PagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPageTable { get; set; }

        public bool IsMasterTable { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Page Page { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageTableField> PageTableFields { get; set; }
    }
}
