using DemoProject.Services.Abstract.PersonInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonDeathViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;

        public PersonDeathViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime DeathDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonDeathMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonDeathPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonDeathDocument
        //public Guid PersonDeathDocumentId { get; set; }

        public byte DocumentTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(150)]
        public string NameAsPerDocument { get; set; }

        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public short SequenceNumber { get; set; }

        public DateTime DateOfIssue { get; set; }

        [StringLength(100)]
        public string IssuingAuthority { get; set; }

        [StringLength(100)]
        public string PlaceOfIssue { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] DocumentPhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [StringLength(1)]
        public string DocumentUploadStatus { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //PersonDeathDocumentMakerChecker

        public long PersonDeathDocumentPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        //Other

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

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        public Guid DocumentTypeId { get; set; }

        // DropdownList

        public List<SelectListItem> DocumentTypeDropdownList
        {
            get
            {
                return personDetailRepository.DocumentTypeDropdownList;
            }
        }

    }
}
