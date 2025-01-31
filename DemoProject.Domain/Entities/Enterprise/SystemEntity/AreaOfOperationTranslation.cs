using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("AreaOfOperationTranslation")]
    public partial class AreaOfOperationTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public byte AreaOfOperationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfAreaOfOperation { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual AreaOfOperation AreaOfOperation { get; set; }
    }
}
