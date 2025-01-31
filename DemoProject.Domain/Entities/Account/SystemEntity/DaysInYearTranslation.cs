using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("DaysInYearTranslation")]
    public partial class DaysInYearTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte DaysInYearPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string TransTitle { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual DaysInYear DaysInYear { get; set; }
    }
}
