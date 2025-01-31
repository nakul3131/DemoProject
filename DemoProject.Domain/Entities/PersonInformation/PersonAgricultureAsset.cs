using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAgricultureAsset")]
    public partial class PersonAgricultureAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAgricultureAsset()
        {
            PersonAgricultureAssetDocuments = new HashSet<PersonAgricultureAssetDocument>();

            PersonAgricultureAssetMakerCheckers = new HashSet<PersonAgricultureAssetMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAgricultureAssetId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte AgricultureLandTypePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string AgricultureLandDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string SurveyNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupNumber { get; set; }

        public short AreaOfLand { get; set; }

        public decimal Volume { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        public decimal OwnershipPercentage { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public bool IsOnlyRainFedTypeIrrigation { get; set; }

        public bool HasCanalRiverIrrigationSource { get; set; }

        public bool HasWellsIrrigationSource { get; set; }

        public bool HasFarmLakeSource { get; set; }

        public decimal AnnualIncomeFromLand { get; set; }

        public bool HasAnyCourtCase { get; set; }

        [Required]
        [StringLength(2500)]
        public string CourtCaseFullDetails { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAgricultureAssetDocument> PersonAgricultureAssetDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAgricultureAssetMakerChecker> PersonAgricultureAssetMakerCheckers { get; set; }
    }
}
