using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeInterestRateViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short MinimumInterestRate { get; set; }

        public short MaximumInterestRate { get; set; }

        public short DefaultInterestRate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeInterestRateMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeInterestRatePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
