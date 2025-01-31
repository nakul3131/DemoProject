using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileLoginDeviceMakerChecker")]
    public partial class UserProfileLoginDeviceMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfileLoginDevicePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public string UserAction { get; set; }

        public string Remark { get; set; }
    
        public virtual UserProfileLoginDevice UserProfileLoginDevice { get; set; }
    }
}
