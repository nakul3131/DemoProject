using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesApplicationTranslation")]
    public partial class SharesApplicationTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesApplicationTranslation()
        {
            SharesApplicationTranslationMakerCheckers = new HashSet<SharesApplicationTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int SharesApplicationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransWitnessName { get; set; }

        [Required]
        [StringLength(500)]
        public string TransBankDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransStatusReason { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SharesApplication SharesApplication { get; set; }

        public virtual Language Language { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationTranslationMakerChecker> SharesApplicationTranslationMakerCheckers { get; set; }
    }
}
