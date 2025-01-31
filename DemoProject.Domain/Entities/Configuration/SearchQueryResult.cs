using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("SearchQueryResult")]
    public partial class SearchQueryResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SearchQueryResult()
        {
            MenuSearchQueryResults = new HashSet<MenuSearchQueryResult>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrmKey { get; set; }

        public Guid SearchQueryResultId { get; set; }

        [Required]
        [StringLength(1500)]
        public string ResultUrl { get; set; }

        [Required]
        [StringLength(1500)]
        public string ShortDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuSearchQueryResult> MenuSearchQueryResults { get; set; }
    }
}
