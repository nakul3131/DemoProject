using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Parameter
{
    public class TransactionParameterViewModel
    {
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;

        public TransactionParameterViewModel()
        {
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public byte PrmKey { get; set; }

        public Guid TransactionParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableTransactionNumberBranchwise { get; set; }
        
        [StringLength(20)]
        public string TransactionParameterNumberMask { get; set; }

        public byte ChecksumAlgorithmPrmKey { get; set; }

        public int StartTransactionNumber { get; set; }

        public int EndTransactionNumber { get; set; }

        public bool EnableAutoGenerateTransactionNumber { get; set; }

        public bool EnableRegenerateUnusedTransactionNumber { get; set; }

        public bool EnableTransactionDigitalCode { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //TransactionParameterMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public byte TransactionParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }
        
        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public Guid ChecksumAlgorithmId { get; set; }

        public List<SelectListItem> ChecksumAlgorithmDropdownList
        {
            get
            {
                return securityDetailRepository.ChecksumAlgorithmDropdownList;
            }
        }

        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FrequencyDropdownList;
            }
        }
    }
}
