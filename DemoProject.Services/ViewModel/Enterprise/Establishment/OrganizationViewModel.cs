using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationViewModel
    {
        // Organization
        public byte PrmKey { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte ModificationNumber { get; set; }

        public short OrganizationType { get; set; }

        [StringLength(100)]
        public string NameOfOrganization { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        [StringLength(1500)]
        public string Motto { get; set; }

        [StringLength(2500)]
        public string Vision { get; set; }

        [StringLength(2500)]
        public string Mission { get; set; }

        [StringLength(2500)]
        public string Standards { get; set; }

        [StringLength(50)]
        public string OrganizationCode { get; set; }

        public DateTime CoopRegistrationDate { get; set; }

        [StringLength(150)]
        public string CoopRegistrationNumber { get; set; }

        public int CoopClassification { get; set; }

        public int CoopSubClassification { get; set; }

        [StringLength(150)]
        public string CoopReferenceNumber { get; set; }

        public DateTime? CoopDateOfClassificationAdvice { get; set; }

        public DateTime? LastElectionDate { get; set; }

        public DateTime RBIRegistrationDate { get; set; }

        [StringLength(150)]
        public string RBIRegistrationNumber { get; set; }

        public int RBIClassification { get; set; }

        public int RBISubClassification { get; set; }

        [StringLength(150)]
        public string RBIReferenceNumber { get; set; }

        public DateTime? RBIDateOfClassificationAdvice { get; set; }

        [StringLength(500)]
        public string RegistrationAddressDetails { get; set; }

        public short CenterPrmKey { get; set; }

        public byte AreaOfOperationPrmKey { get; set; }

        public short LanguageOfBookPrmKey { get; set; }

        [StringLength(10)]
        public string PANNumber { get; set; }

        [StringLength(150)]
        public string OfficialWebSite { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // OrganizationMakerChecker
        public DateTime EntryDateTime { get; set; }

        public byte OrganizationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // OrganizationTranslation
        public Guid OrganizationTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfOrganization { get; set; }

        [StringLength(50)]
        public string TransShortName { get; set; }

        [StringLength(1500)]
        public string TransMotto { get; set; }

        [StringLength(2500)]
        public string TransVision { get; set; }

        [StringLength(2500)]
        public string TransMission { get; set; }

        [StringLength(2500)]
        public string TransStandards { get; set; }

        [StringLength(150)]
        public string TransCoopRegistrationNumber { get; set; }

        [StringLength(150)]
        public string TransCoopReferenceNumber { get; set; }

        [StringLength(150)]
        public string TransRBIRegistrationNumber { get; set; }

        [StringLength(150)]
        public string TransRBIReferenceNumber { get; set; }

        [StringLength(500)]
        public string TransRegistrationAddressDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        // OrganizationTranslationMakerChecker
        public short OrganizationTranslationPrmKey { get; set; }

        // AuthorizedSharesCapitalViewModel
        public AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel { get; set; }

        // OrganizationContactDetailViewModel
        public OrganizationContactDetailViewModel OrganizationContactDetailViewModel { get; set; }

        // OrganizationFundViewModel
        public OrganizationFundViewModel OrganizationFundViewModel { get; set; }

        // OrganizationLoanTypeViewModel
        public OrganizationLoanTypeViewModel OrganizationLoanTypeViewModel { get; set; }

        // OrganizationGSTRegistrationDetailViewModel
        public OrganizationGSTRegistrationDetailViewModel OrganizationGSTRegistrationDetailViewModel { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public string NameOfMaker { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public bool IsRegisterUnderCooperative { get; set; }

        public bool IsRegisterUnderRBI { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem

        public Guid LanguageId { get; set; }

        public Guid AreaOfOperationId { get; set; }

        public Guid OrganizationTypeId { get; set; }

        public Guid CenterId { get; set; }

        public Guid CoopClassificationId { get; set; }

        public Guid CoopSubClassificationId { get; set; }

        public Guid RBIClassificationId { get; set; }

        public Guid RBISubClassificationId { get; set; }

        //public Guid ContactTypeId { get; set; }

        //public Guid ContactGroupId { get; set; }

        //public Guid AccountClassId { get; set; }

        //public Guid FundId { get; set; }

        //public Guid StateId { get; set; }

        //public Guid GSTRegistrationTypeId { get; set; }

        //public Guid GSTReturnPeriodicityId { get; set; }

    }
}
