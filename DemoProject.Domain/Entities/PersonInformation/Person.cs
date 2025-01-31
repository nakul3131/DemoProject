using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            ForeignerPersons = new HashSet<ForeignerPerson>();
            GuardianPersons = new HashSet<GuardianPerson>();
            PersonAdditionalDetails = new HashSet<PersonAdditionalDetail>();
            PersonAdditionalIncomeDetails = new HashSet<PersonAdditionalIncomeDetail>();
            PersonAddresses = new HashSet<PersonAddress>();
            PersonAgricultureAssets = new HashSet<PersonAgricultureAsset>();
            //PersonAssetDetails = new HashSet<PersonAssetDetail>();
            PersonBankDetails = new HashSet<PersonBankDetail>();
            PersonBoardOfDirectorRelations = new HashSet<PersonBoardOfDirectorRelation>();
            PersonBorrowingDetails = new HashSet<PersonBorrowingDetail>();
            PersonChronicDiseases = new HashSet<PersonChronicDisease>();
            PersonCommoditiesAssets = new HashSet<PersonCommoditiesAsset>();
            PersonContactDetails = new HashSet<PersonContactDetail>();
            PersonCourtCases = new HashSet<PersonCourtCase>();
            PersonCreditRatings = new HashSet<PersonCreditRating>();
            //PersonCustomFields = new HashSet<PersonCustomField>();
            //PersonDeaths = new HashSet<PersonDeath>();
            //PersonDocuments = new HashSet<PersonDocument>();
            PersonEmployementDetails = new HashSet<PersonEmploymentDetail>();
            PersonFamilyDetails = new HashSet<PersonFamilyDetail>();
            PersonFinancialAssets = new HashSet<PersonFinancialAsset>();
            PersonGSTRegistrationDetails = new HashSet<PersonGSTRegistrationDetail>();
            PersonHomeBranches = new HashSet<PersonHomeBranch>();
            PersonImmovableAssets = new HashSet<PersonImmovableAsset>();
            PersonIncomeTaxDetails = new HashSet<PersonIncomeTaxDetail>();
            PersonInsuranceDetails = new HashSet<PersonInsuranceDetail>();
            PersonKYCDetails = new HashSet<PersonKYCDetail>();
            PersonMachineryAssets = new HashSet<PersonMachineryAsset>();
            PersonMovableAssets = new HashSet<PersonMovableAsset>();
            PersonMakerCheckers = new HashSet<PersonMakerChecker>();
            PersonPhotoSigns = new HashSet<PersonPhotoSign>();
            PersonSMSAlertes = new HashSet<PersonSMSAlert>();
            PersonSocialMedias = new HashSet<PersonSocialMedia>();
            //PersonPhotoes = new HashSet<PersonPhoto>();
            //PersonPhotoSigns = new HashSet<PersonPhotoSign>();
            PersonPrefixes = new HashSet<PersonPrefix>();
            PersonRelatives = new HashSet<PersonRelative>();
            PersonTranslations = new HashSet<PersonTranslation>();
            PersonGroups = new HashSet<PersonGroup>();
        }

        [Key]
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonInformationNumber { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string MotherName { get; set; }

        [Required]
        [StringLength(50)]
        public string MothersMaidenName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfBirthOnDocument { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForeignerPerson> ForeignerPersons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuardianPerson> GuardianPersons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalDetail> PersonAdditionalDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalIncomeDetail> PersonAdditionalIncomeDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAgricultureAsset> PersonAgricultureAssets { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonAssetDetail> PersonAssetDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBankDetail> PersonBankDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBoardOfDirectorRelation> PersonBoardOfDirectorRelations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBorrowingDetail> PersonBorrowingDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonChronicDisease> PersonChronicDiseases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCommoditiesAsset> PersonCommoditiesAssets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonContactDetail> PersonContactDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCourtCase> PersonCourtCases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCreditRating> PersonCreditRatings { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonCustomField> PersonCustomFields { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonDeath> PersonDeaths { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonDocument> PersonDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonEmploymentDetail> PersonEmployementDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFamilyDetail> PersonFamilyDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFinancialAsset> PersonFinancialAssets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGSTRegistrationDetail> PersonGSTRegistrationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonHomeBranch> PersonHomeBranches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonImmovableAsset> PersonImmovableAssets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonIncomeTaxDetail> PersonIncomeTaxDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInsuranceDetail> PersonInsuranceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonKYCDetail> PersonKYCDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMachineryAsset> PersonMachineryAssets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMovableAsset> PersonMovableAssets { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMakerChecker> PersonMakerCheckers { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonPhotoSign> PersonPhotoSigns { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonSMSAlert> PersonSMSAlertes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonSocialMedia> PersonSocialMedias { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonPhoto> PersonPhotoes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonPhotoSign> PersonPhotoSigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonPrefix> PersonPrefixes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonRelative> PersonRelatives { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonTranslation> PersonTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroup> PersonGroups { get; set; }
    }
}
