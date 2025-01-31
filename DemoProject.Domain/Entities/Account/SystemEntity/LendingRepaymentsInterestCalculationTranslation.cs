using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("LendingRepaymentsInterestCalculationTranslation")]
    public partial class LendingRepaymentsInterestCalculationTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte LendingRepaymentsInterestCalculationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string TransNameOfEvent { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
    }
}
