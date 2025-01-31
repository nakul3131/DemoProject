using DemoProject.Services.Abstract.Management.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class EvaluationSectorContentItemViewModel
    {
        private readonly IContentItemRepository contentItemRepository;
        private readonly IEvaluationSectionRepository evaluationSectionRepository;

        public EvaluationSectorContentItemViewModel()
        {
            contentItemRepository = DependencyResolver.Current.GetService<IContentItemRepository>();
            evaluationSectionRepository = DependencyResolver.Current.GetService<IEvaluationSectionRepository>();
        }

        //EvaluationSectionContentItem

        public short PrmKey { get; set; }

        public Guid EvaluationSectorContentItemId { get; set; }

        public short EvaluationSectionPrmKey { get; set; }

        public short ContentItemPrmKey { get; set; }

        public short SequenceNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //EvaluationSectionContentItemMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short EvaluationSectorContentItemPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public Guid EvaluationSectionId { get; set; }

        public Guid ContentItemId { get; set; }

        [StringLength(100)]
        public string NameOfEvaluationSection { get; set; }

        [StringLength(100)]
        public string NameOfContentItem { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For DropdownList
        public List<SelectListItem> ContentItems
        {
            get
            {
                return contentItemRepository.ContentItemDropdownList;
            }
        }

        public List<SelectListItem> EvaluationSections
        {
            get
            {
                return evaluationSectionRepository.EvaluationSectionDropdownList;
            }
        }

    }
}
