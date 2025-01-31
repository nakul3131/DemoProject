using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
   public class SharesApplicationDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly ISharesApplicationRepository sharesApplicationRepository;

        public SharesApplicationDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            sharesApplicationRepository = DependencyResolver.Current.GetService<ISharesApplicationRepository>();
        }

        //SharesApplicationDetail

        public short PrmKey { get; set; }

        public int SharesApplicationPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte MemberTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

       
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SharesApplicationDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SharesApplicationDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Dropdown

        public Guid BusinessOfficeId { get; set; }

        public Guid MemberTypeId { get; set; }

        public Guid PersonId { get; set; }

        public Guid SchemeId { get; set; }

        public Guid TransactionTypeId { get; set; }

        // DropdownList

        public List<SelectListItem> SharesApplicationDropdownList
        {
            get
            {
                return accountDetailRepository.SharesApplicationDropdownList;
            }
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.NonMemberPersonDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> MemberTypeDropdownList
        {
            get
            {
                return accountDetailRepository.MemberTypeDropdownList;
            }
        }

        public List<SelectListItem> SharesCapitalSchemeDropdownList
        {
            get
            {
                return accountDetailRepository.SharesCapitalSchemeDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }


        public string AllowLastPastDays
        {
            get
            {
                short allowBackDate = securityDetailRepository.GetPastDaysPermissionForTransaction((short)HttpContext.Current.Session["UserProfilePrmKey"], accountDetailRepository.GetPreviousClosingFinancialYearEndDate());
                DateTime dateTime = DateTime.Now.AddDays(-allowBackDate);
                return String.Format("{0:yyyy-MM-dd}", dateTime);
            }
        }
    }
}
