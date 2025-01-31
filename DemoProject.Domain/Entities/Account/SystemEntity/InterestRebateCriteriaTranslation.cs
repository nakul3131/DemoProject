using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestRebateCriteriaTranslation")]
    public partial class InterestRebateCriteriaTranslation
    {
        [Key]
        public byte PrmKey { get; set; }

        public byte InterestRebateCriteriaPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string TransSysNameOfCriteria { get; set; }

        [Required]
        [StringLength(500)]
        public string TransTitle { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual InterestRebateCriteria InterestRebateCriteria { get; set; }
    }
}
