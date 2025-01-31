using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonStatusViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte MemberTypePrmKey { get; set; }

        public byte BorrowingStatus { get; set; }

        public byte GuarantorStatus { get; set; }

        public bool IsActiveMember { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
