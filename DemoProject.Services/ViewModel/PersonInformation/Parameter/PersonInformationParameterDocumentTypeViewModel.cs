using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Parameter
{
    public class PersonInformationParameterDocumentTypeViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public PersonInformationParameterDocumentTypeViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        // PersonInformationParameterDocumentType

        public byte PrmKey { get; set; }

        //public Guid PersonInformationParameterDocumentTypeId { get; set; }

        public byte PersonInformationParameterPrmKey { get; set; }

        public byte DocumentTypePrmKey { get; set; }

        public bool IsMandatory { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PersonInformationParameterDocumentTypeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte PersonInformationParameterDocumentTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem
        public Guid DocumentTypeId { get; set; }

        [StringLength(100)]
        public string NameOfDocumentType { get; set; }

    }
}