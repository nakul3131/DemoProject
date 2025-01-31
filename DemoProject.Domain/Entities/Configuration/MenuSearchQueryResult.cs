using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
   [Table("MenuSearchQueryResult")]
    public partial class MenuSearchQueryResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrmKey { get; set; }

        public Guid MenuSearchQueryResultId { get; set; }

        public int MenuPrmKey { get; set; }

        public int SearchQueryPrmKey { get; set; }

        public int SearchQueryResultPrmKey { get; set; }

        public virtual SearchQuery SearchQuery { get; set; }

        public virtual SearchQueryResult SearchQueryResult { get; set; }
    }
}
