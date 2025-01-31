using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("SearchQuery")]
    public partial class SearchQuery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SearchQuery()
        {
            MenuSearchQueryResults = new HashSet<MenuSearchQueryResult>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrmKey { get; set; }

        public Guid SearchQueryId { get; set; }

        [Required]
        [StringLength(1500)]
        public string QueryText { get; set; }

        public short SequenceNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuSearchQueryResult> MenuSearchQueryResults { get; set; }
    }
}
