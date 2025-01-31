using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("DividendCalculationMethodTranslation")]
    public partial class DividendCalculationMethodTranslation
    {
        [Key]
        public byte PrmKey { get; set; }

        public byte DividendCalculationMethodPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfMethod { get; set; }

        [Required]
        [StringLength(20)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string TransFormula { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
       
    }
}
