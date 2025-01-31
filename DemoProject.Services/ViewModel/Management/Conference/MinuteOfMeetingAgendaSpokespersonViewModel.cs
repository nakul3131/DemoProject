using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MinuteOfMeetingAgendaSpokespersonViewModel
    {
        // MinuteOfMeetingAgendaSpokesperson

        public int PrmKey { get; set; }

        public Guid MinuteOfMeetingAgendaSpokespersonId { get; set; } 

        public int MeetingAgendaPrmKey { get; set; } 

        public short BoardOfDirectorPrmKey { get; set; }

        public TimeSpan SpeakingStartTime { get; set; }

        public TimeSpan SpeakingEndTime { get; set; }

        [StringLength(3500)]
        public string Speech { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // MinuteOfMeetingAgendaSpokespersonMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MinuteOfMeetingAgendaSpokespersonPrmKey { get; set; } 

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // MinuteOfMeetingAgendaSpokespersonTranslation
        public Guid MinuteOfMeetingAgendaSpokespersonTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(3500)]
        public string TransSpeech { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker

        public int MinuteOfMeetingAgendaSpokespersonTranslationPrmKey { get; set; }

        // Other

        public Guid BoardOfDirectorId { get; set; }

        [StringLength(100)]
        public string NameOfBoardOfDirector { get; set; }
    }
}
