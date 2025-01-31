using DemoProject.Services.Abstract.Account.SystemEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountBeneficiaryDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public CustomerAccountBeneficiaryDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        // CustomerAccountBeneficiaryDetail
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        //public DateTime EffectiveDate { get; set; }

        [StringLength(50)]
        public string BeneficiaryCode { get; set; }

        [StringLength(100)]
        public string NameOfBeneficiary { get; set; }

        [StringLength(20)]
        public string ShortName { get; set; }

        public short CustomerAccountTypePrmKey { get; set; }

        public long AccountNumber { get; set; }

        public long AccountNumberConfirm { get; set; }

        [StringLength(50)]
        public string IfscCode { get; set; }

        [StringLength(150)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string Branch { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public int CustomerNumber { get; set; }

        public long MobileNumber { get; set; }

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

        //  CustomerAccountBeneficiaryDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountBeneficiaryDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        [StringLength(100)]
        public string NameOfCustomerAccountType { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid CustomerAccountTypeId { get; set; }

        public List<SelectListItem> CustomerAccountTypeDropdownList => accountDetailRepository.CustomerAccountTypeDropdownList;     
    }
}
