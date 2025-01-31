using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserStatusViewModel
    {
        [StringLength(100)]
        public string UserProfileFullName { get; set; }

        public DateTime? SignInTime { get; set; }

        public DateTime? SignOutTime { get; set; }

        [StringLength(3)]
        public string UserProfileStatus { get; set; }

        public short InvalidSuccessiveAttemptCount { get; set; }

        public short InvalidCumulativeAttemptCount { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short UserProfilePrmKey { get; set; }

        public IEnumerable<SelectListItem> Status
        {
            get
            {
                return new[]
                {
                    new SelectListItem {Value ="ALL", Text = "All"},
                    new SelectListItem {Value ="RDY", Text= "Ready"},
                    new SelectListItem {Value ="LGN", Text= "Logged In"},
                    new SelectListItem {Value ="CLS", Text= "Closed" },
                    new SelectListItem {Value ="LOK", Text = "Locked"},
                    new SelectListItem {Value ="ABS", Text= "Absent"},
                    new SelectListItem {Value ="EXP", Text= "Expired" }
                };
            }
        }

    }
}
