using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.Password
{
    public class PasswordPolicyValueViewModel
    {
        public byte MinimumPasswordLength { get; set; }

        public byte MaximumPasswordLength { get; set; }

        public byte MinimumNumberOfUpperCaseCharacters { get; set; }

        public byte MinimumNumberOfLowerCaseCharacters { get; set; }

        public byte MinimumNumberOfSpecialCaseCharacters { get; set; }

        public byte MinimumNumberOfNumericCharacters { get; set; }

        public byte MinimumNumberOfRepetitiveCharacters { get; set; }

        public byte ForcePasswordChangeAfterDays { get; set; }

        public short ReusePreviousPassword { get; set; }

        public short MinimumDaysForReusePreviousPassword { get; set; }

        public byte PasswordExpiryAlertDays { get; set; }
    }
}
