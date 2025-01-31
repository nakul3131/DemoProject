using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Layout;
using DemoProject.Domain.Entities.Account.SystemEntity;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesApplicationDetail")]
    public partial class SharesApplicationDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesApplicationDetail()
        {
            SharesApplicationDetailMakerCheckers = new HashSet<SharesApplicationDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public int SharesApplicationPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte MemberTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SharesApplication SharesApplication { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual MemberType MemberType { get; set; }

        public virtual Person Person { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationDetailMakerChecker> SharesApplicationDetailMakerCheckers { get; set; }
    }
}
