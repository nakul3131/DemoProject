using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CenterDemographicDetail")]
    public partial class CenterDemographicDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterDemographicDetail()
        {
            CenterDemographicDetailMakerCheckers = new HashSet<CenterDemographicDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterDemographicDetailId { get; set; }

        public short CenterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte LocalGovernmentPrmKey { get; set; }

        public byte DirectionPrmKey { get; set; }

        public byte AreaTypePrmKey { get; set; }

        public long TotalPopulation { get; set; }

        public decimal PerCapitaIncome { get; set; }

        public byte EducationLevelPrmKey { get; set; }

        public byte FamilySystemPrmKey { get; set; }

        public long NumberOfResidentsOwningHomes { get; set; }

        public int Pincode { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        public virtual LocalGovernment LocalGovernment { get; set; }

        public virtual Direction Direction { get; set; }

        public virtual AreaType AreaType { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }

        public virtual FamilySystem FamilySystem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterDemographicDetailMakerChecker> CenterDemographicDetailMakerCheckers { get; set; }
    }
}
