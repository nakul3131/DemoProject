using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Parameter
{
    [Table("PersonInformationParameter")]
    public partial class PersonInformationParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonInformationParameter()
        {
            PersonInformationParameterMakerCheckers = new HashSet<PersonInformationParameterMakerChecker>();
            PersonInformationParameterDocumentTypes = new HashSet<PersonInformationParameterDocumentType>();
            PersonInformationParameterNoticeTypes = new HashSet<PersonInformationParameterNoticeType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        //public Guid PersonInformationParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableAutoGeneratePersonInformationNumber { get; set; }

        public bool EnablePersonInformationNumberBranchwise { get; set; }

        [StringLength(20)]
        public string PersonInformationNumberMask { get; set; }

        public byte ChecksumAlgorithmPrmKey { get; set; }

        public long StartPersonInformationNumber { get; set; }

        public long EndPersonInformationNumber { get; set; }

        public int PersonInformationNumberIncrementBy { get; set; }

        public bool EnableRandomPersonInformationNumber { get; set; }

        public bool EnableRegenerateUnusedPersonInformationNumber { get; set; }

        public bool EnablePersonInformationDigitalCode { get; set; }

        public bool EnableFamilyDetails { get; set; }

        public bool EnablePowerOfAttorney { get; set; }

        public bool EnableChronicDisease { get; set; }

        public bool EnableSocialMedia { get; set; }

        public bool EnableInsuranceDetail { get; set; }

        public bool EnableGSTRegistration { get; set; }

        public bool EnableIncomeTaxDetail { get; set; }

        public bool EnableBankingDetail { get; set; }

        public bool EnableCommoditiesAsset { get; set; }

        public bool EnableFinancialAsset { get; set; }

        public bool EnableMovableAsset { get; set; }

        public bool EnableImmovableAsset { get; set; }

        public bool EnableAgricultureAsset { get; set; }

        public bool EnableMachineryAsset { get; set; }

        public bool EnableAdditionalIncomeDetail { get; set; }

        public bool EnableBorrowingDetail { get; set; }

        public bool EnableCourtCaseDetail { get; set; }

        public bool EnableCreditRating { get; set; }

        public bool EnableSMSAlert { get; set; }

        public bool EnablePersonInformationGroup { get; set; }

        public bool EnableMobileOTPForVerification { get; set; }

        [StringLength(3)]
        public string VerificationMobileOTPDataType { get; set; }

        public byte VerificationMobileOTPLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForVerificationMobileOTP { get; set; }

        [StringLength(10)]
        public string PostfixStringForVerificationMobileOTP { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForVerificationMobileOTP { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForVerificationMobileOTP { get; set; }

        public TimeSpan VerificationMobileOTPExpiryTime { get; set; }

        public byte MaximumResendForVerificationMobileOTP { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string PhotoDocumentUpload { get; set; }

        public bool EnablePhotoDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInDb { get; set; }

        public bool EnablePhotoDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string PhotoDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string SignDocumentUpload { get; set; }

        public bool EnableSignDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInDb { get; set; }

        public bool EnableSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string SignDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string KYCDocumentUpload { get; set; }

        public bool EnableKYCDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string KYCDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForKYCDocumentUploadInDb { get; set; }

        public bool EnableKYCDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string KYCDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string KYCDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForKYCDocumentUploadInLocalStorage { get; set; }

        public byte LowRiskPersonKYCReVerificationTimeInYear { get; set; }

        public byte MediumRiskPersonKYCReVerificationTimeInYear { get; set; }

        public byte HighRiskPersonKYCReVerificationTimeInYear { get; set; }

        public byte VeryHighRiskPersonKYCReVerificationTimeInYear { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string GSTDocumentUpload { get; set; }

        public bool EnableGSTDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string GSTDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForGSTDocumentUploadInDb { get; set; }

        public bool EnableGSTDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string GSTDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string GSTDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForGSTDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string IncomeTaxDocumentUpload { get; set; }

        public bool EnableIncomeTaxDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string IncomeTaxDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForIncomeTaxDocumentUploadInDb { get; set; }

        public bool EnableIncomeTaxDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string IncomeTaxDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string IncomeTaxDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForIncomeTaxDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string BankStatementDocumentUpload { get; set; }

        public bool EnableBankStatementDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string BankStatementDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForBankStatementDocumentUploadInDb { get; set; }

        public bool EnableBankStatementDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string BankStatementDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string BankStatementDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForBankStatementDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string FinancialAssetDocumentUpload { get; set; }

        public bool EnableFinancialAssetDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string FinancialAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForFinancialAssetDocumentUploadInDb { get; set; }

        public bool EnableFinancialAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string FinancialAssetDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string FinancialAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForFinancialAssetDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string MovableAssetDocumentUpload { get; set; }

        public bool EnableMovableAssetDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string MovableAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForMovableAssetDocumentUploadInDb { get; set; }

        public bool EnableMovableAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string MovableAssetDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string MovableAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForMovableAssetDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string ImmovableAssetDocumentUpload { get; set; }

        public bool EnableImmovableAssetDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string ImmovableAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForImmovableAssetDocumentUploadInDb { get; set; }

        public bool EnableImmovableAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string ImmovableAssetDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string ImmovableAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForImmovableAssetDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string AgricultureAssetDocumentUpload { get; set; }

        public bool EnableAgricultureAssetDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string AgricultureAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForAgricultureAssetDocumentUploadInDb { get; set; }

        public bool EnableAgricultureAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string AgricultureAssetDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string AgricultureAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForAgricultureAssetDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string MachineryAssetDocumentUpload { get; set; }

        public bool EnableMachineryAssetDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string MachineryAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForMachineryAssetDocumentUploadInDb { get; set; }

        public bool EnableMachineryAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string MachineryAssetDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string MachineryAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForMachineryAssetDocumentUploadInLocalStorage { get; set; }

        [Required(ErrorMessage = "Please Select At Least One Option")]
        [StringLength(1)]
        public string DeathDocumentUpload { get; set; }

        public bool EnableDeathDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string DeathDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForDeathDocumentUploadInDb { get; set; }

        public bool EnableDeathDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string DeathDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string DeathDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForDeathDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInformationParameterMakerChecker> PersonInformationParameterMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInformationParameterDocumentType> PersonInformationParameterDocumentTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInformationParameterNoticeType> PersonInformationParameterNoticeTypes { get; set; }
    }
}