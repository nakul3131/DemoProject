using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountPhotoSign")]
    public partial class CustomerAccountPhotoSign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountPhotoSign()
        {
            CustomerAccountPhotoSignMakerCheckers = new HashSet<CustomerAccountPhotoSignMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoNameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoFileCaption { get; set; }

        [Required]
        public byte[] PhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string PhotoLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string SignNameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string SignFileCaption { get; set; }

        [Required]
        public byte[] SignPhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string SignLocalStoragePath { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountPhotoSignMakerChecker> CustomerAccountPhotoSignMakerCheckers { get; set; }
    }
}
