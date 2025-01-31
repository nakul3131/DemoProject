using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class PowerAndFunctionViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPowerAndFunctionRepository powerAndFunctionRepository;

        public PowerAndFunctionViewModel()
        {
            powerAndFunctionRepository = DependencyResolver.Current.GetService<IPowerAndFunctionRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // PowerAndFunction
        public int PrmKey { get; set; }

        public Guid PowerAndFunctionId { get; set; }

        [StringLength(3)]
        public string PowerAndFunctionFor { get; set; }

        public Guid PowerAndFunctionForId { get; set; }

        [StringLength(4000)]
        public string NameOfPowerAndFunction { get; set; }

        [StringLength(150)]
        public string AliasName { get; set; }

        [StringLength(1000)]
        public string NameOnReport { get; set; }

        public int ParentPrmKey { get; set; }

        public Guid ParentId { get; set; }

        public bool IsTitle { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //PowerAndFunctionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //PowerAndFunctionTranslation

        public Guid PowerAndFunctionTranslationId { get; set; }

        public int PowerAndFunctionPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(4000)]
        public string TransNameOfPowerAndFunction { get; set; }

        [StringLength(150)]
        public string TransAliasName { get; set; }

        [StringLength(1000)]
        public string TransNameOnReport { get; set; }

        [StringLength(500)]
        public string TransNote { get; set; }

        //PowerAndFunctionTranslationMakerChecker
        public int PowerAndFunctionTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(4000)]
        public string NameOfPowerAndFunctionInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Power And Function");
            }
        }

        [StringLength(100)]
        public string NameOfPowerAndFunctionPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Power And Function");
            }
        }

        [StringLength(10)]
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

        [StringLength(50)]
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

        [StringLength(4000)]
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

        //Other
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> PowerAndFunctionCategories
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Committy" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Committy"),
                        Value = "CMT"
                    },
                    new SelectListItem
                    {
                        Text="Board Of Director" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Board Of Director"),
                        Value = "BED"
                    },
                    new SelectListItem
                    {
                        Text="Chairman" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Chairman"),
                        Value = "CM"
                    },
                    new SelectListItem
                    {
                        Text="Vice Chairman" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Vice Chairman"),
                        Value = "VCM"
                    },
                    new SelectListItem
                    {
                        Text="CEO" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("CEO"),
                        Value = "CO"
                    },
                    new SelectListItem
                    {
                        Text="Manager" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Manager"),
                        Value = "MG"
                    },
                };
            }
        }

        public List<SelectListItem> Parents
        {
            get
            {
                return powerAndFunctionRepository.Parents;
            }

        }
    }
}
