using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class LoanSchemeIndexViewModel
    {
        public short PrmKey { get; set; }

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

    }
}
