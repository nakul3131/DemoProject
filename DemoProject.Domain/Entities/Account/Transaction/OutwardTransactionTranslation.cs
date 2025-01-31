using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OutwardTransactionTranslation")]
    public partial class OutwardTransactionTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OutwardTransactionTranslation()
        {
            OutwardTransactionTranslationMakerCheckers = new HashSet<OutwardTransactionTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int OutwardTransactionPrmkey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNumberOnOutwardDocument { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransOutwardSubject { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReceiverName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReceiverAddress { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransCourierServiceDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual OutwardTransaction OutwardTransaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionTranslationMakerChecker> OutwardTransactionTranslationMakerCheckers { get; set; }
    }
}
