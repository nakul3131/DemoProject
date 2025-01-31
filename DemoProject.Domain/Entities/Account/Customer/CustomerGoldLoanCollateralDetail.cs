using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Master;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerGoldLoanCollateralDetail")]
    public partial class CustomerGoldLoanCollateralDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerGoldLoanCollateralDetail()
        {
            CustomerGoldLoanCollateralDetailMakerCheckers = new HashSet<CustomerGoldLoanCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short JewelAssayerPrmKey { get; set; }

        public short GoldOrnamentPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string MetalPurity { get; set; }

        [Required]
        [StringLength(12)]
        public string HUID { get; set; }

        public int GoldLoanRatePrmKey { get; set; }

        public byte Qty { get; set; }

        public decimal MetalGrossWeight { get; set; }

        public bool HasAnyDamage { get; set; }

        public decimal DamageWeight { get; set; }

        [Required]
        [StringLength(1500)]
        public string DamageDescription { get; set; }

        public bool HasAnyWestage { get; set; }

        public decimal WestageWeight { get; set; }

        [Required]
        [StringLength(1500)]
        public string WestageDescription { get; set; }

        public bool HasDiamond { get; set; }

        public bool IsDiamondDeductable { get; set; }

        public byte NumberOfDiamond { get; set; }

        public decimal DiamondCarat { get; set; }

        [Required]
        [StringLength(150)]
        public string ClarityColour { get; set; }

        public decimal DiamondWeight { get; set; }

        public decimal DiamondPrice { get; set; }

        public decimal DiamondValuation { get; set; }

        public decimal MetalNetWeight { get; set; }

        [Required]
        [StringLength(3)]
        public string CustodyStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string JewelAssayerRemark { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        public virtual GoldLoanRate GoldLoanRate { get; set; }

        public virtual GoldOrnament GoldOrnament { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanCollateralDetailMakerChecker> CustomerGoldLoanCollateralDetailMakerCheckers { get; set; }
    }
}
