using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class BoardOfDirectorPowerAndFunctionViewModel
    {
        private readonly IBoardOfDirectorRepository boardOfDirectorRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPowerAndFunctionRepository powerAndFunctionRepository;
        
        public BoardOfDirectorPowerAndFunctionViewModel()
        {
            boardOfDirectorRepository = DependencyResolver.Current.GetService<IBoardOfDirectorRepository>();
            powerAndFunctionRepository = DependencyResolver.Current.GetService<IPowerAndFunctionRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        //BoardOfDirectorPowerAndFunction
        public int PrmKey { get; set; }

        public Guid BoardOfDirectorPowerAndFunctionId { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public int PowerAndFunctionPrmKey { get; set; }

        public Guid PowerAndFunctionId { get; set; }

        public short SequenceNumber { get; set; }

        [StringLength(50)]
        public string SequenceText { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //BoardOfDirectorPowerAndFunctionMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //BoardOfDirectorPowerAndFunctionTranslation

        public Guid BoardOfDirectorPowerAndFunctionTranslationId { get; set; }

        public int BoardOfDirectorPowerAndFunctionPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(50)]
        public string TransSequenceText { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //BoardOfDirectorPowerAndFunctionMakerChecker
        public int BoardOfDirectorPowerAndFunctionTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(50)]
        public string SequenceTextInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Sequence Text");
            }
        }

        [StringLength(100)]
        public string SequenceTextPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Sequence Text");
            }
        }

        [StringLength(1500)]
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
        
        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> BoardOfDirectors
        {
            get
            {
                return boardOfDirectorRepository.BoardOfDirectorDropdownList;
            }

        }

        public List<SelectListItem> PowerAndFunctions
        {
            get
            {
                return powerAndFunctionRepository.PowerAndFunctionDropdownList;
            }

        }
    }
}
