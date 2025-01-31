using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerGoldLoanCollateralDetailViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerGoldLoanCollateralDetailId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short JewelAssayerPrmKey { get; set; }

        public short GoldOrnamentPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(3)]
        public string MetalPurity { get; set; }

        [StringLength(12)]
        public string HUID { get; set; }

        public int GoldLoanRatePrmKey { get; set; }

        public byte Qty { get; set; }

        public decimal MetalGrossWeight { get; set; }

        public decimal GoldValuationAmount { get; set; }
        public bool HasAnyDamage { get; set; }

        public decimal DamageWeight { get; set; }

        [StringLength(1500)]
        public string DamageDescription { get; set; }

        public bool HasAnyWestage { get; set; }

        public decimal WestageWeight { get; set; }

        [StringLength(1500)]
        public string WestageDescription { get; set; }

        public bool HasDiamond { get; set; }

        public bool IsDiamondDeductable { get; set; }

        public byte NumberOfDiamond { get; set; }

        public decimal DiamondCarat { get; set; }

        [StringLength(150)]
        public string ClarityColour { get; set; }

        public decimal DiamondWeight { get; set; }

        public decimal DiamondPrice { get; set; }

        public decimal DiamondValuation { get; set; }

        public decimal MetalNetWeight { get; set; }

        [StringLength(3)]
        public string CustodyStatus { get; set; }

        [StringLength(1500)]
        public string JewelAssayerRemark { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerGoldLoanCollateralDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerGoldLoanCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public decimal LoanAmountPerGram { get; set; }

        public decimal ValuationAmount { get; set; }

        public Guid JewelAssayerId { get; set; }

        [StringLength(100)]
        public string NameOfJewelAssayer { get; set; }

        public Guid GoldOrnamentId { get; set; }

        [StringLength(100)]
        public string NameOfGoldOrnament { get; set; }

        public Guid GoldLoanRateId { get; set; }

        [StringLength(100)]
        public string NameOfGoldLoanRate { get; set; }

        [StringLength(100)]
        public string MetalPurityText { get; set; }

        [StringLength(100)]
        public string CustodyStatusText { get; set; }
    }
}
