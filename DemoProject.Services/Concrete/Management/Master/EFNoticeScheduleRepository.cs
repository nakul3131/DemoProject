using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Master;
using DemoProject.Domain.Entities.Master.General.Notice;
using DemoProject.Services.ViewModel.Master.General.Notice;
using DemoProject.Services.Abstract.Master.General.Notice;

namespace DemoProject.Services.Concrete.Master
{
    public class EFNoticeScheduleRepository : INoticeScheduleRepository
    {
        private readonly EFDbContext context;
        private readonly IWeekMonthDayScheduleRepository weekMonthDayScheduleRepository;

        public EFNoticeScheduleRepository(RepositoryConnection _connection, IWeekMonthDayScheduleRepository _weekMonthDayScheduleRepository)
        {
            context = _connection.EFDbContext;
            weekMonthDayScheduleRepository = _weekMonthDayScheduleRepository;
        }

        public async Task<bool> Amend(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Amend
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Amend;
                _noticeScheduleViewModel.EntryStatus = StringLiteralValue.Amend;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.ReasonForModification = "None";
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.TransReasonForModification = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _noticeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                NoticeSchedule noticeSchedule = Mapper.Map<NoticeSchedule>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);
                noticeScheduleTranslation.PrmKey = _noticeScheduleViewModel.NoticeScheduleTranslationPrmKey;

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);
                noticeScheduleOnDate.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;
                noticeScheduleOnDate.EntryStatus = StringLiteralValue.Verify;

                //day

                NoticeScheduleOnDateTime noticeScheduleOnDateTimeForAmend = Mapper.Map<NoticeScheduleOnDateTime>(_noticeScheduleViewModel);
                noticeScheduleOnDateTimeForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTimeForAmend);
                context.Entry(noticeScheduleOnDateTimeForAmend).State = EntityState.Modified;

                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();

                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(viewModel);
                    noticeScheduleOnDateTime.NoticeScheduleOnDatePrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;
                    noticeScheduleOnDateTime.PrmKey = 0; 

                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);
                    noticeScheduleOnDateTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDateTimeMakerChecker.PrmKey = 0;
                    noticeScheduleOnDateTimeMakerChecker.NoticeScheduleOnDateTimePrmKey = 0;

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDateTime.NoticeScheduleOnDateTimeMakerCheckers.Add(noticeScheduleOnDateTimeMakerChecker);

                    context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                    context.Entry(noticeScheduleOnDateTime).State = EntityState.Added;
                }

                //month
                

                NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonthForAmend = Mapper.Map<NoticeScheduleOnDaysOfMonth>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthForAmend.EntryStatus = StringLiteralValue.Verify;
                
                context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonthForAmend);
                context.Entry(noticeScheduleOnDaysOfMonthForAmend).State = EntityState.Modified;

                short NoticeSchedulePrmKey = GetPrmKeyById(_noticeScheduleViewModel.NoticeScheduleId);
                IEnumerable<MonthScheduleViewModel> organizationContactDetailViewModelListForDelete = await weekMonthDayScheduleRepository.GetMonthRejectedEntries(NoticeSchedulePrmKey);
                
                foreach (MonthScheduleViewModel monthScheduleViewModel in organizationContactDetailViewModelListForDelete)
                {
                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTimeForAmend1 = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(monthScheduleViewModel);

                    noticeScheduleOnDaysOfMonthTimeForAmend1.PrmKey = monthScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTimeForAmend1);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeForAmend1).State = EntityState.Deleted;

                    break;
                }

                NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTimeForAmend = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthTimeForAmend.NoticeScheduleOnDaysOfMonthPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;
                noticeScheduleOnDaysOfMonthTimeForAmend.EntryStatus = StringLiteralValue.Verify;
                
                context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTimeForAmend);
                context.Entry(noticeScheduleOnDaysOfMonthTimeForAmend).State = EntityState.Modified;

                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();

                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];

                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {

                    //viewModel.DayOfMonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthDayId);
                    //viewModel.MonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthId);

                    viewModel.MonthInterval = _noticeScheduleViewModel.MonthInterval;

                    NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(viewModel);
                    noticeScheduleOnDaysOfMonth.NoticeSchedulePrmKey = _noticeScheduleViewModel.NoticeSchedulePrmKey;
                    noticeScheduleOnDaysOfMonth.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfMonthMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfMonthMakerChecker.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(viewModel);
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;
                    noticeScheduleOnDaysOfMonthTime.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfMonthTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfMonthTimeMakerChecker.PrmKey = 0;

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Add(noticeScheduleOnDaysOfMonthTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                    context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthTimes.Add(noticeScheduleOnDaysOfMonthTime);

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthMakerCheckers.Add(noticeScheduleOnDaysOfMonthMakerChecker);

                    context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                    context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Added;

                    noticeSchedule.NoticeScheduleOnDaysOfMonths.Add(noticeScheduleOnDaysOfMonth);
                }

                //week

                NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeekForAmend = Mapper.Map<NoticeScheduleOnDaysOfWeek>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeekForAmend);
                context.Entry(noticeScheduleOnDaysOfWeekForAmend).State = EntityState.Modified;

                NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTimeForAmend = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekTimeForAmend.NoticeScheduleOnDaysOfWeekPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;
                noticeScheduleOnDaysOfWeekTimeForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTimeForAmend);
                context.Entry(noticeScheduleOnDaysOfWeekTimeForAmend).State = EntityState.Modified;

                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();

                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];

                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {

                    //viewModel.DayOfWeekPrmKey = categoryRepository.GetPrmKeyById(viewModel.WeekDayId);
                    viewModel.WeekInterval = _noticeScheduleViewModel.WeekInterval;

                    NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(viewModel);
                    noticeScheduleOnDaysOfWeek.NoticeSchedulePrmKey = _noticeScheduleViewModel.NoticeSchedulePrmKey;
                    noticeScheduleOnDaysOfWeek.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfWeekMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfWeekMakerChecker.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(viewModel);
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;
                    noticeScheduleOnDaysOfWeekTime.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfWeekTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfWeekTimeMakerChecker.PrmKey = 0;

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Add(noticeScheduleOnDaysOfWeekTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                    context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekTimes.Add(noticeScheduleOnDaysOfWeekTime);

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekMakerCheckers.Add(noticeScheduleOnDaysOfWeekMakerChecker);

                    context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                    context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Added;

                    noticeSchedule.NoticeScheduleOnDaysOfWeeks.Add(noticeScheduleOnDaysOfWeek);
                }

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Modified;

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Modified;

                context.NoticeSchedules.Attach(noticeSchedule);
                context.Entry(noticeSchedule).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> AmendModification(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Amend
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Amend;
                _noticeScheduleViewModel.EntryStatus = StringLiteralValue.Amend;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.ReasonForModification = "None";
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.TransReasonForModification = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _noticeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                NoticeScheduleModification noticeScheduleModification = Mapper.Map<NoticeScheduleModification>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);
                noticeScheduleTranslation.PrmKey = _noticeScheduleViewModel.NoticeScheduleTranslationPrmKey;

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);
                noticeScheduleOnDate.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;
                noticeScheduleOnDate.EntryStatus = StringLiteralValue.Verify;

                //day

                NoticeScheduleOnDateTime noticeScheduleOnDateTimeForAmend = Mapper.Map<NoticeScheduleOnDateTime>(_noticeScheduleViewModel);
                noticeScheduleOnDateTimeForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTimeForAmend);
                context.Entry(noticeScheduleOnDateTimeForAmend).State = EntityState.Modified;

                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();

                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(viewModel);
                    noticeScheduleOnDateTime.NoticeScheduleOnDatePrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;
                    noticeScheduleOnDateTime.PrmKey = 0;

                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);
                    noticeScheduleOnDateTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDateTimeMakerChecker.PrmKey = 0;
                    noticeScheduleOnDateTimeMakerChecker.NoticeScheduleOnDateTimePrmKey = 0;

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDateTime.NoticeScheduleOnDateTimeMakerCheckers.Add(noticeScheduleOnDateTimeMakerChecker);

                    context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                    context.Entry(noticeScheduleOnDateTime).State = EntityState.Added;
                }

                //month


                NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonthForAmend = Mapper.Map<NoticeScheduleOnDaysOfMonth>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonthForAmend);
                context.Entry(noticeScheduleOnDaysOfMonthForAmend).State = EntityState.Modified;

                short NoticeSchedulePrmKey = GetPrmKeyById(_noticeScheduleViewModel.NoticeScheduleId);
                IEnumerable<MonthScheduleViewModel> organizationContactDetailViewModelListForDelete = await weekMonthDayScheduleRepository.GetMonthRejectedEntries(NoticeSchedulePrmKey);

                foreach (MonthScheduleViewModel monthScheduleViewModel in organizationContactDetailViewModelListForDelete)
                {
                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTimeForAmend1 = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(monthScheduleViewModel);

                    noticeScheduleOnDaysOfMonthTimeForAmend1.PrmKey = monthScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTimeForAmend1);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeForAmend1).State = EntityState.Deleted;

                    break;
                }

                NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTimeForAmend = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthTimeForAmend.NoticeScheduleOnDaysOfMonthPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;
                noticeScheduleOnDaysOfMonthTimeForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTimeForAmend);
                context.Entry(noticeScheduleOnDaysOfMonthTimeForAmend).State = EntityState.Modified;

                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();

                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];

                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {

                    //viewModel.DayOfMonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthDayId);
                    //viewModel.MonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthId);

                    viewModel.MonthInterval = _noticeScheduleViewModel.MonthInterval;

                    NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(viewModel);
                    noticeScheduleOnDaysOfMonth.NoticeSchedulePrmKey = _noticeScheduleViewModel.NoticeSchedulePrmKey;
                    noticeScheduleOnDaysOfMonth.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfMonthMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfMonthMakerChecker.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(viewModel);
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;
                    noticeScheduleOnDaysOfMonthTime.PrmKey = 0;

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfMonthTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfMonthTimeMakerChecker.PrmKey = 0;

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Add(noticeScheduleOnDaysOfMonthTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                    context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthTimes.Add(noticeScheduleOnDaysOfMonthTime);

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthMakerCheckers.Add(noticeScheduleOnDaysOfMonthMakerChecker);

                    context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                    context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Added;
                }

                //week

                NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeekForAmend = Mapper.Map<NoticeScheduleOnDaysOfWeek>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeekForAmend);
                context.Entry(noticeScheduleOnDaysOfWeekForAmend).State = EntityState.Modified;

                NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTimeForAmend = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekTimeForAmend.NoticeScheduleOnDaysOfWeekPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;
                noticeScheduleOnDaysOfWeekTimeForAmend.EntryStatus = StringLiteralValue.Verify;

                context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTimeForAmend);
                context.Entry(noticeScheduleOnDaysOfWeekTimeForAmend).State = EntityState.Modified;

                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();

                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];

                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {

                    //viewModel.DayOfWeekPrmKey = categoryRepository.GetPrmKeyById(viewModel.WeekDayId);
                    viewModel.WeekInterval = _noticeScheduleViewModel.WeekInterval;

                    NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(viewModel);
                    noticeScheduleOnDaysOfWeek.NoticeSchedulePrmKey = _noticeScheduleViewModel.NoticeSchedulePrmKey;
                    noticeScheduleOnDaysOfWeek.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfWeekMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfWeekMakerChecker.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(viewModel);
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekPrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;
                    noticeScheduleOnDaysOfWeekTime.PrmKey = 0;

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);
                    noticeScheduleOnDaysOfWeekTimeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    noticeScheduleOnDaysOfWeekTimeMakerChecker.PrmKey = 0;

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Add(noticeScheduleOnDaysOfWeekTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                    context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekTimes.Add(noticeScheduleOnDaysOfWeekTime);

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekMakerCheckers.Add(noticeScheduleOnDaysOfWeekMakerChecker);

                    context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                    context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Added;
                }

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Modified;

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Modified;

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Delete
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Delete;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeSchedule noticeSchedule = Mapper.Map<NoticeSchedule>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);
                noticeScheduleTranslation.PrmKey = _noticeScheduleViewModel.NoticeScheduleTranslationPrmKey;

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);
                noticeScheduleOnDate.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;

                NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(_noticeScheduleViewModel);
                noticeScheduleOnDateTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDateTimePrmKey;

                NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonth.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;

                NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthTimePrmKey;

                NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeek.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;

                NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekTimePrmKey;

                context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Deleted;

                context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                context.Entry(noticeScheduleOnDateTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Deleted;

                context.NoticeSchedules.Attach(noticeSchedule);
                context.Entry(noticeSchedule).State = EntityState.Deleted;

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Deleted;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> DeleteModification(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Delete
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Delete;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeScheduleModification noticeScheduleModification = Mapper.Map<NoticeScheduleModification>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);
                noticeScheduleTranslation.PrmKey = _noticeScheduleViewModel.NoticeScheduleTranslationPrmKey;

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);
                noticeScheduleOnDate.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDatePrmKey;

                NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(_noticeScheduleViewModel);
                noticeScheduleOnDateTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDateTimePrmKey;

                NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonth.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthPrmKey;

                NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfMonthTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfMonthTimePrmKey;

                NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeek.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekPrmKey;

                NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(_noticeScheduleViewModel);
                noticeScheduleOnDaysOfWeekTime.PrmKey = _noticeScheduleViewModel.NoticeScheduleOnDaysOfWeekTimePrmKey;

                context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Deleted;

                context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                context.Entry(noticeScheduleOnDateTime).State = EntityState.Deleted;

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Deleted;

                context.NoticeScheduleModifications.Attach(noticeScheduleModification);
                context.Entry(noticeScheduleModification).State = EntityState.Deleted;

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Deleted;


                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPrmKeyById(Guid _NoticeScheduleId)
        {
            return context.NoticeSchedules
                    .Where(c => c.NoticeScheduleId == _NoticeScheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> NoticeScheduleDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.NoticeSchedules
                            join mf in context.NoticeScheduleModifications on b.PrmKey equals mf.NoticeSchedulePrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.NoticeScheduleTranslations on b.PrmKey equals t.NoticeSchedulePrmKey into bt
                            from t in bt.DefaultIfEmpty()

                            where (b.EntryStatus.Equals(StringLiteralValue.Verify)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (b.IsModified.Equals(false))
                                    && t.LanguagePrmKey == regionalLanguagePrmKey)

                            select new SelectListItem
                            {
                                Value = b.NoticeScheduleId.ToString(),
                                Text = ((mf.NameOfNoticeSchedule.Equals(null)) ? b.NameOfNoticeSchedule.Trim() + " ---> " + (t.TransNameOfNoticeSchedule.Equals(null) ? " " : t.TransNameOfNoticeSchedule.Trim()) : mf.NameOfNoticeSchedule + " ---> " + (t.TransNameOfNoticeSchedule.Equals(null) ? " " : t.TransNameOfNoticeSchedule.Trim()))
                            }).ToList();


                }

                return (from b in context.NoticeSchedules
                        join mf in context.NoticeScheduleModifications on b.PrmKey equals mf.NoticeSchedulePrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (b.EntryStatus.Equals(StringLiteralValue.Verify)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = b.NoticeScheduleId.ToString(),
                            Text = ((mf.NameOfNoticeSchedule.Equals(null)) ? b.NameOfNoticeSchedule.Trim() : mf.NameOfNoticeSchedule)
                        }).ToList();
            }
        }

        public async Task<NoticeScheduleViewModel> GetRejectedEntry(Guid _noticeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntry (@NoticeScheduleId)", new SqlParameter("@NoticeScheduleId", _noticeScheduleId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<NoticeScheduleViewModel> GetUnVerifiedEntry(Guid _noticeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntry (@NoticeScheduleId)", new SqlParameter("@NoticeScheduleId", _noticeScheduleId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<NoticeScheduleViewModel> GetVerifiedEntry(Guid _noticeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<NoticeScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleEntry (@NoticeScheduleId)", new SqlParameter("@NoticeScheduleId", _noticeScheduleId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Reject
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Reject;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeScheduleMakerChecker noticeScheduleMakerChecker = Mapper.Map<NoticeScheduleMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();

                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                }

                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();

                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];

                List<NoticeScheduleOnDaysOfMonth> NoticeScheduleOnDaysOfMonthList = new List<NoticeScheduleOnDaysOfMonth>();

                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                }

                //week
                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();
                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];
                
                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                }

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;

                context.NoticeScheduleMakerCheckers.Attach(noticeScheduleMakerChecker);
                context.Entry(noticeScheduleMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> RejectModification(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Reject
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Reject;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeScheduleModificationMakerChecker noticeScheduleModificationMakerChecker = Mapper.Map<NoticeScheduleModificationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();
                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                }

                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();
                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];
                
                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                }

                //week
                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();
                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];
                
                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                }

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Create
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Create;
                _noticeScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.ReasonForModification = "None";
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.TransReasonForModification = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _noticeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                NoticeSchedule noticeSchedule = Mapper.Map<NoticeSchedule>(_noticeScheduleViewModel);

                NoticeScheduleMakerChecker noticeScheduleMakerChecker = Mapper.Map<NoticeScheduleMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);

                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;
                noticeScheduleOnDate.NoticeScheduleOnDateMakerCheckers.Add(noticeScheduleOnDateMakerChecker);

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Added;
                noticeSchedule.NoticeScheduleOnDates.Add(noticeScheduleOnDate);

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();
                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];
                
                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(viewModel);

                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDateTime.NoticeScheduleOnDateTimeMakerCheckers.Add(noticeScheduleOnDateTimeMakerChecker);

                    context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                    context.Entry(noticeScheduleOnDateTime).State = EntityState.Added;

                    noticeScheduleOnDate.NoticeScheduleOnDateTimes.Add(noticeScheduleOnDateTime);
                }


                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();
                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];
                
                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    //viewModel.DayOfMonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthDayId);
                    //viewModel.MonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthId);

                    viewModel.MonthInterval = _noticeScheduleViewModel.MonthInterval;

                    NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(viewModel);

                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(_noticeScheduleViewModel);
                    
                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Add(noticeScheduleOnDaysOfMonthTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                    context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthTimes.Add(noticeScheduleOnDaysOfMonthTime);
                    
                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthMakerCheckers.Add(noticeScheduleOnDaysOfMonthMakerChecker);

                    context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                    context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Added;

                    noticeSchedule.NoticeScheduleOnDaysOfMonths.Add(noticeScheduleOnDaysOfMonth);
                }

                //week
                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();
                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];
                
                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    //viewModel.DayOfWeekPrmKey = categoryRepository.GetPrmKeyById(viewModel.WeekDayId);
                    viewModel.WeekInterval = _noticeScheduleViewModel.WeekInterval;

                    NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(viewModel);

                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(_noticeScheduleViewModel);

                    NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Add(noticeScheduleOnDaysOfWeekTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                    context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekTimes.Add(noticeScheduleOnDaysOfWeekTime);

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekMakerCheckers.Add(noticeScheduleOnDaysOfWeekMakerChecker);

                    context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                    context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Added;

                    noticeSchedule.NoticeScheduleOnDaysOfWeeks.Add(noticeScheduleOnDaysOfWeek);
                }

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;
                noticeScheduleTranslation.NoticeScheduleTranslationMakerCheckers.Add(noticeScheduleTranslationMakerChecker);

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Added;
                noticeSchedule.NoticeScheduleTranslations.Add(noticeScheduleTranslation);

                context.NoticeScheduleMakerCheckers.Attach(noticeScheduleMakerChecker);
                context.Entry(noticeScheduleMakerChecker).State = EntityState.Added;
                noticeSchedule.NoticeScheduleMakerCheckers.Add(noticeScheduleMakerChecker);

                context.NoticeSchedules.Attach(noticeSchedule);
                context.Entry(noticeSchedule).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> SaveModification(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Create
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Create;
                _noticeScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.ReasonForModification = "None";
                _noticeScheduleViewModel.Remark = "None";
                _noticeScheduleViewModel.TransReasonForModification = "None";
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _noticeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                NoticeScheduleModification noticeScheduleModification = Mapper.Map<NoticeScheduleModification>(_noticeScheduleViewModel);

                NoticeScheduleModificationMakerChecker noticeScheduleModificationMakerChecker = Mapper.Map<NoticeScheduleModificationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslation noticeScheduleTranslation = Mapper.Map<NoticeScheduleTranslation>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleOnDate noticeScheduleOnDate = Mapper.Map<NoticeScheduleOnDate>(_noticeScheduleViewModel);

                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;
                noticeScheduleOnDate.NoticeScheduleOnDateMakerCheckers.Add(noticeScheduleOnDateMakerChecker);

                context.NoticeScheduleOnDates.Attach(noticeScheduleOnDate);
                context.Entry(noticeScheduleOnDate).State = EntityState.Added;

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();
                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];
                
                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    NoticeScheduleOnDateTime noticeScheduleOnDateTime = Mapper.Map<NoticeScheduleOnDateTime>(viewModel);

                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDateTime.NoticeScheduleOnDateTimeMakerCheckers.Add(noticeScheduleOnDateTimeMakerChecker);

                    context.NoticeScheduleOnDateTimes.Attach(noticeScheduleOnDateTime);
                    context.Entry(noticeScheduleOnDateTime).State = EntityState.Added;

                    noticeScheduleOnDate.NoticeScheduleOnDateTimes.Add(noticeScheduleOnDateTime);
                }


                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();
                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];

                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    //viewModel.DayOfMonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthDayId);
                    //viewModel.MonthPrmKey = categoryRepository.GetPrmKeyById(viewModel.MonthId);

                    viewModel.MonthInterval = _noticeScheduleViewModel.MonthInterval;

                    NoticeScheduleOnDaysOfMonth noticeScheduleOnDaysOfMonth = Mapper.Map<NoticeScheduleOnDaysOfMonth>(viewModel);

                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(_noticeScheduleViewModel);

                    NoticeScheduleOnDaysOfMonthTime noticeScheduleOnDaysOfMonthTime = Mapper.Map<NoticeScheduleOnDaysOfMonthTime>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonthTime.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Add(noticeScheduleOnDaysOfMonthTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfMonthTimes.Attach(noticeScheduleOnDaysOfMonthTime);
                    context.Entry(noticeScheduleOnDaysOfMonthTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthTimes.Add(noticeScheduleOnDaysOfMonthTime);

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfMonth.NoticeScheduleOnDaysOfMonthMakerCheckers.Add(noticeScheduleOnDaysOfMonthMakerChecker);

                    context.NoticeScheduleOnDaysOfMonths.Attach(noticeScheduleOnDaysOfMonth);
                    context.Entry(noticeScheduleOnDaysOfMonth).State = EntityState.Added;
                }

                //week
                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();
                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];
                
                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    //viewModel.DayOfWeekPrmKey = categoryRepository.GetPrmKeyById(viewModel.WeekDayId);
                    viewModel.WeekInterval = _noticeScheduleViewModel.WeekInterval;

                    NoticeScheduleOnDaysOfWeek noticeScheduleOnDaysOfWeek = Mapper.Map<NoticeScheduleOnDaysOfWeek>(viewModel);

                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(_noticeScheduleViewModel);

                    NoticeScheduleOnDaysOfWeekTime noticeScheduleOnDaysOfWeekTime = Mapper.Map<NoticeScheduleOnDaysOfWeekTime>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(_noticeScheduleViewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeekTime.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Add(noticeScheduleOnDaysOfWeekTimeMakerChecker);

                    context.NoticeScheduleOnDaysOfWeekTimes.Attach(noticeScheduleOnDaysOfWeekTime);
                    context.Entry(noticeScheduleOnDaysOfWeekTime).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekTimes.Add(noticeScheduleOnDaysOfWeekTime);

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                    noticeScheduleOnDaysOfWeek.NoticeScheduleOnDaysOfWeekMakerCheckers.Add(noticeScheduleOnDaysOfWeekMakerChecker);

                    context.NoticeScheduleOnDaysOfWeeks.Attach(noticeScheduleOnDaysOfWeek);
                    context.Entry(noticeScheduleOnDaysOfWeek).State = EntityState.Added;
                }

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;
                noticeScheduleTranslation.NoticeScheduleTranslationMakerCheckers.Add(noticeScheduleTranslationMakerChecker);

                context.NoticeScheduleTranslations.Attach(noticeScheduleTranslation);
                context.Entry(noticeScheduleTranslation).State = EntityState.Added;

                context.NoticeScheduleModificationMakerCheckers.Attach(noticeScheduleModificationMakerChecker);
                context.Entry(noticeScheduleModificationMakerChecker).State = EntityState.Added;
                noticeScheduleModification.NoticeScheduleModificationMakerCheckers.Add(noticeScheduleModificationMakerChecker);

                context.NoticeScheduleModifications.Attach(noticeScheduleModification);
                context.Entry(noticeScheduleModification).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Verify
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Verify;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeScheduleMakerChecker noticeScheduleMakerChecker = Mapper.Map<NoticeScheduleMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();

                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = "C";
                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                }

                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();

                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];

                List<NoticeScheduleOnDaysOfMonth> NoticeScheduleOnDaysOfMonthList = new List<NoticeScheduleOnDaysOfMonth>();

                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = "C";
                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                }

                //week

                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();

                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];

                List<NoticeScheduleOnDaysOfWeek> NoticeScheduleOnDaysOfWeekList = new List<NoticeScheduleOnDaysOfWeek>();

                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = "C";
                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                }

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;

                context.NoticeScheduleMakerCheckers.Attach(noticeScheduleMakerChecker);
                context.Entry(noticeScheduleMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyModification(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            try
            {
                // Set EntryStatus And UserAction As Verify
                _noticeScheduleViewModel.UserAction = StringLiteralValue.Verify;
                _noticeScheduleViewModel.EntryDateTime = DateTime.Now;
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                NoticeScheduleModificationMakerChecker noticeScheduleModificationMakerChecker = Mapper.Map<NoticeScheduleModificationMakerChecker>(_noticeScheduleViewModel);

                NoticeScheduleTranslationMakerChecker noticeScheduleTranslationMakerChecker = Mapper.Map<NoticeScheduleTranslationMakerChecker>(_noticeScheduleViewModel);
                NoticeScheduleOnDateMakerChecker noticeScheduleOnDateMakerChecker = Mapper.Map<NoticeScheduleOnDateMakerChecker>(_noticeScheduleViewModel);

                //day
                List<DayScheduleViewModel> dayScheduleViewModelList = new List<DayScheduleViewModel>();
                dayScheduleViewModelList = (List<DayScheduleViewModel>)HttpContext.Current.Session["DaySchedule"];

                foreach (DayScheduleViewModel viewModel in dayScheduleViewModelList)
                {
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    NoticeScheduleOnDateTimeMakerChecker noticeScheduleOnDateTimeMakerChecker = Mapper.Map<NoticeScheduleOnDateTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDateTimeMakerCheckers.Attach(noticeScheduleOnDateTimeMakerChecker);
                    context.Entry(noticeScheduleOnDateTimeMakerChecker).State = EntityState.Added;
                }

                //month
                List<MonthScheduleViewModel> monthScheduleViewModelList = new List<MonthScheduleViewModel>();
                monthScheduleViewModelList = (List<MonthScheduleViewModel>)HttpContext.Current.Session["MonthSchedule"];
                
                foreach (MonthScheduleViewModel viewModel in monthScheduleViewModelList)
                {
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    NoticeScheduleOnDaysOfMonthMakerChecker noticeScheduleOnDaysOfMonthMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfMonthTimeMakerChecker noticeScheduleOnDaysOfMonthTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfMonthTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfMonthTimeMakerCheckers.Attach(noticeScheduleOnDaysOfMonthTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfMonthMakerCheckers.Attach(noticeScheduleOnDaysOfMonthMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfMonthMakerChecker).State = EntityState.Added;
                }

                //week
                List<WeekScheduleViewModel> weekScheduleViewModelList = new List<WeekScheduleViewModel>();
                weekScheduleViewModelList = (List<WeekScheduleViewModel>)HttpContext.Current.Session["WeekSchedule"];
                
                foreach (WeekScheduleViewModel viewModel in weekScheduleViewModelList)
                {
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    NoticeScheduleOnDaysOfWeekMakerChecker noticeScheduleOnDaysOfWeekMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekMakerChecker>(viewModel);

                    NoticeScheduleOnDaysOfWeekTimeMakerChecker noticeScheduleOnDaysOfWeekTimeMakerChecker = Mapper.Map<NoticeScheduleOnDaysOfWeekTimeMakerChecker>(viewModel);

                    context.NoticeScheduleOnDaysOfWeekTimeMakerCheckers.Attach(noticeScheduleOnDaysOfWeekTimeMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekTimeMakerChecker).State = EntityState.Added;

                    context.NoticeScheduleOnDaysOfWeekMakerCheckers.Attach(noticeScheduleOnDaysOfWeekMakerChecker);
                    context.Entry(noticeScheduleOnDaysOfWeekMakerChecker).State = EntityState.Added;
                }

                context.NoticeScheduleOnDateMakerCheckers.Attach(noticeScheduleOnDateMakerChecker);
                context.Entry(noticeScheduleOnDateMakerChecker).State = EntityState.Added;

                context.NoticeScheduleTranslationMakerCheckers.Attach(noticeScheduleTranslationMakerChecker);
                context.Entry(noticeScheduleTranslationMakerChecker).State = EntityState.Added;

                context.NoticeScheduleModificationMakerCheckers.Attach(noticeScheduleModificationMakerChecker);
                context.Entry(noticeScheduleModificationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
