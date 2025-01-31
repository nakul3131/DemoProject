using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.CustomEntities
{
    public class CustomInvalidLoginLog
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EffectiveDateTime { get; set; }

        public string InputedUserName { get; set; }

        public string InputedPassword { get; set; }

        public short UserProfilePrmKey { get; set; }

        public string ClientMachineName { get; set; }

        public string ClientBrowser { get; set; }

        public string ClientIPAddress { get; set; }

        public string ClientLocation { get; set; }

        public string ClientApp { get; set; }

        public string ClientOperatingSystem { get; set; }

        public short MatchingRatioOfInputedPassword { get; set; }
    }
}
