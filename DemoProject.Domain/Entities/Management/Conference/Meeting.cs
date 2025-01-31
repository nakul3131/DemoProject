using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("Meeting")]
    public partial class Meeting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meeting()
        {
            MeetingAgendas = new HashSet<MeetingAgenda>();
            MeetingInviteeBoardOfDirectors = new HashSet<MeetingInviteeBoardOfDirector>();
            MeetingInviteeMembers = new HashSet<MeetingInviteeMember>();
            MeetingMakerCheckers = new HashSet<MeetingMakerChecker>();
            MeetingModifications = new HashSet<MeetingModification>();
            MeetingNotices = new HashSet<MeetingNotice>();
            MeetingTranslations = new HashSet<MeetingTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid MeetingId { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(3500)]
        public string Objective { get; set; }

        [Required]
        [StringLength(1500)]
        public string FullAddress { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public bool IsAllowancePayble { get; set; }

        public bool IsPayByCash { get; set; }

        public TimeSpan CommencementTime { get; set; }

        public TimeSpan AdjournmentTime { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime NextMeetingDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAgenda> MeetingAgendas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingInviteeBoardOfDirector> MeetingInviteeBoardOfDirectors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingInviteeMember>  MeetingInviteeMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingMakerChecker> MeetingMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingModification> MeetingModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingNotice> MeetingNotices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingTranslation> MeetingTranslations { get; set; }
    }
}
