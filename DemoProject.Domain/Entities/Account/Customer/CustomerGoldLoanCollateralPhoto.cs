using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{

    [Table("CustomerGoldLoanCollateralPhoto")]
    public partial class CustomerGoldLoanCollateralPhoto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerGoldLoanCollateralPhoto()
        {
            CustomerGoldLoanCollateralPhotoMakerCheckers = new HashSet<CustomerGoldLoanCollateralPhotoMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string PhotoType { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoCaption { get; set; }

        [Required]
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [Required]
        public byte[] PhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanCollateralPhotoMakerChecker> CustomerGoldLoanCollateralPhotoMakerCheckers { get; set; }
    }
}
