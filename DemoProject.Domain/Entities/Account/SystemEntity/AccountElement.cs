using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("AccountElement")]
    public partial class AccountElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountElement()
        {
            AccountClasses = new HashSet<AccountClass>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short PrmKey { get; set; }

        [Required]
        [StringLength(30)]
        public string NameOfElement { get; set; }

        [Required]
        [StringLength(2)]
        public string EffectWhenIncrease { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(6)]
        public string AlphabeticElementCode { get; set; }

        public short NumericElementCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountClass> AccountClasses  { get; set; }
    }
}
