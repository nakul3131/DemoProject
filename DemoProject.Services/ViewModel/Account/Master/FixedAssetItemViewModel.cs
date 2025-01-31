using DemoProject.Services.Abstract.MachineLearning;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class FixedAssetItemViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;

        public FixedAssetItemViewModel()
        {
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // FixedAssetItem

        public short PrmKey { get; set; }

        public Guid FixedAssetItemId { get; set; }

        [StringLength(150)]
        public string NameOfItem { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(150)]
        public string NameOnReport { get; set; }

        public bool IsTangibleAsset { get; set; }

        public bool IsTaxable { get; set; }

        public short HSN_SACCode { get; set; }

        public decimal IGST { get; set; }

        public decimal CGST { get; set; }

        public decimal SGST { get; set; }

        public decimal Cess { get; set; }

        public bool IsEligibleForITC { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // FixedAssetItemMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short FixedAssetItemPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // FixedAssetItemModification

        public Guid FixedAssetItemModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // FixedAssetItemModificationMakerChecker

        public short FixedAssetItemModificationPrmKey { get; set; }

        // FixedAssetItemTranslation

        public Guid FixedAssetItemTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransNameOfItem { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(150)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //  FixedAssetItemTranslationMakerChecker
        public short FixedAssetItemTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfItemInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Item");
            }
        }

        [StringLength(100)]
        public string NameOfItemPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Item");
            }
        }

        [StringLength(100)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
