using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterIsoCodeViewModel
    {
        // CenterISOCode

        public short PrmKey { get; set; }

        public Guid CenterISOCodeId { get; set; }

        public short CenterPrmKey { get; set; }

        [StringLength(2)]
        public string ISOAlphaNumericCode2 { get; set; }

        [StringLength(3)]
        public string ISOAlphaNumericCode3 { get; set; }

        public short ISONumericCode { get; set; }

        [StringLength(20)]
        public string OtherCode { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CenterISOCodeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short CenterISOCodePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Center

        public Guid CenterId { get; set; }

        [StringLength(100)]
        public string NameOfCenter { get; set; }
    }
}
