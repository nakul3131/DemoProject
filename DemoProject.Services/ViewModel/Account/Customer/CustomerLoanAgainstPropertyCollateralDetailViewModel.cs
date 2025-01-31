using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
  public class CustomerLoanAgainstPropertyCollateralDetailViewModel
  {
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short CenterPrmKey { get; set; }

        public byte PropertyAge { get; set; }

        public decimal EstimatedPropertyValue { get; set; }

        public decimal DownPaymentAmount { get; set; }

        [StringLength(3)]
        public string PropertyUsage { get; set; }

        public bool HasExistingPropertyLiabilities { get; set; }

        public decimal OutstandingLoanAmount { get; set; }

        public short RemainingTerm { get; set; }

        public decimal MonthlyRepaymentAmount { get; set; }

        public bool IsPropertyLegallyRegistered { get; set; }

        public bool IsPropertyFreeOfAnyLegalDisputes { get; set; }

        public bool HasMortgageInsurance { get; set; }

        public decimal MortgageInsuranceAmount { get; set; }

        [StringLength(3)]
        public string PropertyType { get; set; }

        [StringLength(500)]
        public string PropertyAddressLine1 { get; set; }

        [StringLength(500)]
        public string PropertyAddressLine2 { get; set; }

        [StringLength(500)]
        public string PropertyAddressLine3 { get; set; }

        [StringLength(1500)]
        public string PropertyProximityToKeyLandmarks { get; set; }

        [StringLength(3)]
        public string PropertyOwnershipStatus { get; set; }

        [StringLength(150)]
        public string LenderName { get; set; }

        [StringLength(1500)]
        public string AnyAdditionalLiens { get; set; }

        [StringLength(1500)]
        public string LegalDisputeDetails { get; set; }

        [StringLength(3)]
        public string PropertyCondition { get; set; }

        [StringLength(1500)]
        public string NeighborhoodInformation { get; set; }

        [StringLength(500)]
        public string TransportConnectivity { get; set; }

        [StringLength(500)]
        public string SecurityFeatures { get; set; }

        [StringLength(500)]
        public string UtilityAvailability { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //MakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAgainstPropertyCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(30)]
        public string PropertyTypeOther { get; set; }

        [StringLength(30)]
        public string PropertyOwnershipStatusOther { get; set; }

        //other
        public Guid CenterId { get; set; }

    }
}
