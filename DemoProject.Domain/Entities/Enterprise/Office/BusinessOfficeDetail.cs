using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Domain.Entities.Enterprise.Schedule;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Domain.Entities.Account.SystemEntity;
using DemoProject.Domain.Entities.Enterprise.SystemEntity;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeDetail")]
    public partial class BusinessOfficeDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeDetail()
        {
            BusinessOfficeDetailMakerCheckers = new HashSet<BusinessOfficeDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

     //   public Guid BusinessOfficeDetailId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short CenterPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public byte OfficeSchedulePrmKey { get; set; }

        public short BusinessOfficeTypePrmKey { get; set; }

        public byte BusinessNaturePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public short CommandAreaRadius { get; set; }

        public int PopulationOfTheCommandArea { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAuthorization { get; set; }

        [Required]
        [StringLength(3)]
        public string GeneralLedgerGroupCode { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessNature BusinessNature { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        public virtual BusinessOfficeType BusinessOfficeType { get; set; }

        public virtual Center Center { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        public virtual OfficeSchedule OfficeSchedule { get; set; }

        public virtual Language Language { get; set; }
         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDetailMakerChecker> BusinessOfficeDetailMakerCheckers { get; set; }
    }
}
