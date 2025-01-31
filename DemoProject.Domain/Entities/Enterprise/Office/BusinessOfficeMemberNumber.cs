using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeMemberNumber")]
    public partial class BusinessOfficeMemberNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeMemberNumber()
        {
            BusinessOfficeMemberNumberMakerCheckers = new HashSet<BusinessOfficeMemberNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoMemberNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string MemberNumberMask { get; set; }

        public int StartMemberNumber { get; set; }

        public int EndMemberNumber { get; set; }

        public int MemberNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedMemberNumber { get; set; }

        public bool EnableRandomMemberNumber { get; set; }

        public bool EnableCustomizeMemberNumber { get; set; }

        public bool EnableDigitalCodeForMemberNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeMemberNumberMakerChecker> BusinessOfficeMemberNumberMakerCheckers { get; set; }
    }
}
