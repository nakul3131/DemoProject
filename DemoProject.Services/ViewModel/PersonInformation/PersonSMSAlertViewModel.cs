using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonSMSAlertViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short PersonInformationParameterNoticeTypePrmKey { get; set; }

        public short AppLanguagePrmKey { get; set; }

        public TimeSpan SendingTime { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonSMSAlertMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonSMSAlertPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid AppLanguageId { get; set; }

        [StringLength(50)]
        public string NameOfAppLanguage { get; set; }

        public Guid PersonInformationParameterNoticeTypeId { get; set; }

        public Guid NoticeTypeId { get; set; }

        [StringLength(50)]
        public string NameOfNoticeType { get; set; }

    }
}
