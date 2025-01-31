using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MinuteOfMeetingAgendaViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public MinuteOfMeetingAgendaViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // MinuteOfMeetingAgenda

        public int PrmKey { get; set; }

        public Guid MinuteOfMeetingAgendaId { get; set; }

        public int MeetingAgendaPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public TimeSpan AgendaStartTime { get; set; }

        public TimeSpan AgendaEndTime { get; set; }

        [StringLength(3)]
        public string Vote { get; set; }

        [StringLength(3500)]
        public string Resolution { get; set; }

        [StringLength(50)]
        public string ResolutionNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // MinuteOfMeetingAgendaMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // MinuteOfMeetingAgendaSpokesperson

        public Guid MinuteOfMeetingAgendaSpokespersonId { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        public TimeSpan SpeakingStartTime { get; set; }

        public TimeSpan SpeakingEndTime { get; set; }

        [StringLength(3500)]
        public string Speech { get; set; }

        // MinuteOfMeetingAgendaSpokespersonMakerChecker

        public int MinuteOfMeetingAgendaSpokespersonPrmKey { get; set; }

        // MinuteOfMeetingAgendaSpokespersonTranslation

        public Guid MinuteOfMeetingAgendaSpokespersonTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(3500)]
        public string TransSpeech { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker

        public int MinuteOfMeetingAgendaSpokespersonTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(3500)]
        public string SpeechInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Speech");
            }
        }

        [StringLength(100)]
        public string SpeechPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Speech");
            }
        }

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        // Other
        public Guid MeetingAgendaId { get; set; }

        public string NameOfAgenda { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public string NameOfBoardOfDirector { get; set; }

        public Guid MeetingId { get; set; }

        public string MeetingTitle { get; set; }

        public string Title { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // DropDown 

        public List<SelectListItem> BoardOfDirectorDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDropdownList;
            }
        }
    }
}
