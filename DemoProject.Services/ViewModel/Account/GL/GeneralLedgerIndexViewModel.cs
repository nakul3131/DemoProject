using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerIndexViewModel
    {
        public short PrmKey { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }
    }
}
