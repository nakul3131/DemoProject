using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileLoginDeviceViewModel
    {
        public short PrmKey { get; set; }

        public Guid UserProfileLoginDeviceId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short LoginDevicePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //UserProfileLoginDeviceMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfileLoginDevicePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid LoginDeviceId { get; set; }

        [StringLength(100)]
        public string NameOfDevice { get; set; }
    }
}
