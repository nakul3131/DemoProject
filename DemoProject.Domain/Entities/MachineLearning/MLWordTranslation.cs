using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLWordTranslation")]
    public partial class MLWordTranslation
    {
        [Key]
        public long PrmKey { get; set; }

        public int MLWordPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string WordTranslation { get; set; }

        public virtual MLWord MLWord { get; set; }
    }

}
