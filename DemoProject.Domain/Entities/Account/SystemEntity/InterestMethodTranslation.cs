using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestMethodTranslation")]
    public partial class InterestMethodTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransNameOfMethod { get; set; }

        [Required]
        [StringLength(100)]
        public string TransFormula { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNote { get; set; }

        public virtual InterestMethod InterestMethod { get; set; }
    }
}
