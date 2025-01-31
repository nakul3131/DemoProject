using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("ChequeBookMaster")]
    public partial class ChequeBookMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChequeBookMaster()
        {
            ChequeBookMasterMakerCheckers = new HashSet<ChequeBookMasterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid ChequeBookMasterId { get; set; }

        public int ChequeBookNumber { get; set; }

        public int FirstChequeNumber { get; set; }

        public short TotalNumberOfChequeLeaves { get; set; }

        public DateTime ChequeExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChequeBookMasterMakerChecker> ChequeBookMasterMakerCheckers { get; set; }
    }
}
