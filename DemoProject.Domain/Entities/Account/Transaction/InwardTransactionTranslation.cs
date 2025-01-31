using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("InwardTransactionTranslation")]
    public partial class InwardTransactionTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InwardTransactionTranslation()
        {
          InwardTransactionTranslationMakerCheckers = new HashSet<InwardTransactionTranslationMakerChecker>();
        }
        [Key]
        public int PrmKey { get; set; }

        public int InwardTransactionPrmkey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNumberOnInwardDocument { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransInwardSubject { get; set; }

        [Required]
        [StringLength(4500)]
        public string TransShortDescription { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransSenderName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransSenderAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string TransDeliveryAgencyDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    
        public virtual InwardTransaction InwardTransaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionTranslationMakerChecker> InwardTransactionTranslationMakerCheckers { get; set; }
    }
}
