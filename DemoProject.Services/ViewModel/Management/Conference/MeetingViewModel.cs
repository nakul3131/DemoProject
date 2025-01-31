using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MeetingViewModel
    {
        private readonly IAgendaRepository agendaRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public MeetingViewModel() 
        {
            agendaRepository = DependencyResolver.Current.GetService<IAgendaRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
        }

        // Meeting

        public int PrmKey { get; set; }

        public Guid MeetingId { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(3500)]
        public string Objective { get; set; }

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

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        // MeetingMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MeetingPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // MeetingModification

        public Guid MeetingModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // MeetingModificationMakerChecker

        public int MeetingModificationPrmKey { get; set; }

        // MeetingTranslation

        public Guid MeetingTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(500)]
        public string TransTitle { get; set; }

        [StringLength(3500)]
        public string TransObjective { get; set; }

        [StringLength(1500)]
        public string TransFullAddress { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        // MeetingTranslationMakerChecker

        public int MeetingTranslationPrmKey { get; set; }

        // MeetingInviteeBoardOfDirector
        public Guid MeetingInviteeBoardOfDirectorId { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        [StringLength(50)]
        public string NoticeReferenceNumber { get; set; }

        [StringLength(3)]
        public string NoticeStatus { get; set; }

        [StringLength(3)]
        public string AttendanceStatus { get; set; }

        // MeetingInviteeBoardOfDirectorMakerChecker

        public int MeetingInviteeBoardOfDirectorPrmKey { get; set; }

        // MeetingAgenda

        public Guid MeetingAgendaId { get; set; }

        public int AgendaPrmKey { get; set; }

        public short SequenceNumber { get; set; }

        [StringLength(50)]
        public string SuggestiveMemberNumber { get; set; }

        [StringLength(50)]
        public string PermissiveMemberNumber { get; set; }

        // MeetingAgendaMakerChecker

        public int MeetingAgendaPrmKey { get; set; }

        // MeetingInviteeMember

        public Guid MeetingInviteeMemberId { get; set; }

        public int CustomerSharesCapitalAccountPrmKey { get; set; }

        // MeetingAgendaMakerChecker
        public int MeetingInviteeMemberPrmKey { get; set; }

        // MeetingNotice

        public Guid MeetingNoticeId { get; set; }

        public short NoticeMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        public int MenuPrmKey { get; set; } 

        // MeetingNoticeMakerChecker

        public int MeetingNoticePrmKey { get; set; }

        [StringLength(500)]
        public string TitleInRegionalLanguage 
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Title");
            }
        }

        [StringLength(100)]
        public string TitlePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Title");
            }
        }

        [StringLength(100)]
        public string ObjectiveInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Objective");
            }
        }

        [StringLength(100)]
        public string ObjectivePlaceHolderInRegionalLanguage 
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Objective");
            }
        }

        [StringLength(100)]
        public string FullAddressInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Address");
            }
        }

        [StringLength(100)]
        public string FullAddressPlaceHolderInRegionalLanguage 
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Address");
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

        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

       
        // Other
 
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }


        // For SelectListItem
        public Guid AgendaId { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public Guid CustomerSharesCapitalAccountId { get; set; }

        public Guid MeetingTypeId { get; set; }

        public Guid ScheduleId { get; set; }

        public Guid MenuId { get; set; }

        public Guid NoticeMediaId { get; set; }

        public List<SelectListItem> AgendaDropdownList
        {
            get
            {
                return agendaRepository.AgendaDropdownList;
            }
        }

        public List<SelectListItem> BoardOfDirectorDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDropdownList;
            }
        }

        public List<SelectListItem> CustomerSharesCapitalAccountDropdownList
        {
            get
            {
                return managementDetailRepository.CustomerSharesCapitalAccountDropdownList;
            }
        }

        public List<SelectListItem> MeetingTypeDropdownList
        {
            get
            {
                return managementDetailRepository.MeetingTypeDropdownList;
            }
        }

        public List<SelectListItem> NoticeMediaDropdownList
        {
            get
            {
                return managementDetailRepository.NoticeMediaDropdownList;
            }
        }

        public List<SelectListItem> ScheduleDropdownList
        {
            get
            {
                return enterpriseDetailRepository.ScheduleDropdownList;
            }
        }

        public List<SelectListItem> NoticeFormatDropdownList
        {
            get
            {
                return configurationDetailRepository.ModelMenuDropdownList;
            }
        }
    }
}
