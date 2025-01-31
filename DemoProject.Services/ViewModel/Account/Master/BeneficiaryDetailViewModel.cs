using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Master;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class BeneficiaryDetailViewModel
    {
        private readonly IBeneficiaryRepository beneficiaryRepository;

        public BeneficiaryDetailViewModel() 
        {
            beneficiaryRepository = DependencyResolver.Current.GetService<IBeneficiaryRepository>();
        }

        // BeneficiaryDetail
        public int PrmKey { get; set; }

        public Guid BeneficiaryDetailId { get; set; }

        public DateTime EffectiveDate { get; set; }

        [StringLength(50)]
        public string BeneficiaryCode { get; set; }

        [StringLength(100)]
        public string NameOfBeneficiary { get; set; }

        [StringLength(20)]
        public string ShortName { get; set; }

        public short CustomerAccountTypePrmKey { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public string AccountNumber1 { get; set; }

        [StringLength(50)]
        public string IfscCode { get; set; }

        [StringLength(10)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string Branch { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        public int CustomerNumber { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }

        [StringLength(250)]
        public string EmailId { get; set; }

        [StringLength(250)]
        public string VirtualPrivateAddress { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // BeneficiaryDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int BeneficiaryDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid CustomerAccountTypeId { get; set; }
        
        public List<SelectListItem> CustomerAccountTypeDropdownList 
        {
            get
            {
                return beneficiaryRepository.CustomerAccountTypeDropdownList;
            }
        }
    }
}
