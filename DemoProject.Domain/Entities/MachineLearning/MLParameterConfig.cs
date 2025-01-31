using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLParameterConfig")]
    public partial class MLParameterConfig
    {
        [Key]
        public short PrmKey { get; set; }

        public short MLChapterPointPrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfParameter { get; set; }

        [Required]
        [StringLength(150)]
        public string ConfigValue { get; set; }

        [Required]
        [StringLength(3500)]
        public string Note { get; set; }

        public virtual MLChapterPoint MLChapterPoint { get; set; }
    }
}
