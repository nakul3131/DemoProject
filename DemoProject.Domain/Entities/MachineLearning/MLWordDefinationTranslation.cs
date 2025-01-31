using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLWordDefinationTranslation")]
    public partial class MLWordDefinationTranslation
    {
        [Key]

        public int PrmKey { get; set; }

        public int MLWordDefinationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(4000)]
        public string MLWordTranslation { get; set; }

        public virtual MLWordDefination MLWordDefination { get; set; }
    }
}
