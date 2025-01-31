using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingInviteeMakerChecker")]
    public partial class MeetingInviteeBoardOfDirectorMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int MeetingInviteeBoardOfDirectorPrmKey { get; set; } 

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual MeetingInviteeBoardOfDirector MeetingInviteeBoardOfDirector { get; set; }
    }
}
