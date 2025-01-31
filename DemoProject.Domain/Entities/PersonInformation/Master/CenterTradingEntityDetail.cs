using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CenterTradingEntityDetail")]
    public partial class CenterTradingEntityDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterTradingEntityDetail()
        {
            CenterTradingEntityDetailMakerCheckers = new HashSet<CenterTradingEntityDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterTradingEntityDetailId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short TradingEntityPrmKey { get; set; }

        public long Volume { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterTradingEntityDetailMakerChecker> CenterTradingEntityDetailMakerCheckers { get; set; }
    }
}
