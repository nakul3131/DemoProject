using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerSharesCapitalAccountViewModel
    {
        // CustomerSharesCapitalAccount

        public int PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public int MemberNumber { get; set; }

        public bool IsOtherSocietyMember { get; set; }

        public bool IsCompletedCooperativeEducation { get; set; }

        [StringLength(3)]
        public string MembershipStatus { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CustomerSharesCapitalAccountMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerSharesCapitalAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid MinuteOfMeetingAgendaId { get; set; }

        public List<SelectListItem> MinuteOfMeetingAgendaDropdownList1 { get; set; }

        public bool EnableAllservices { get; set; }
    }
}
