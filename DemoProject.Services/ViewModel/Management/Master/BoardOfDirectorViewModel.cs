using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class BoardOfDirectorViewModel
    {
        private readonly IBoardOfDirectorRepository boardOfDirectorRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly ICustomerAccountRepository customerAccountRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public BoardOfDirectorViewModel()
        {
            boardOfDirectorRepository = DependencyResolver.Current.GetService<IBoardOfDirectorRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            customerAccountRepository = DependencyResolver.Current.GetService<ICustomerAccountRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        public short PrmKey { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public short DesignationPrmKey { get; set; }

        public Guid DesignationId { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(20)]
        public string SequenceNumberText { get; set; }

        public DateTime DateOfAppointment { get; set; }

        public DateTime? DateOfTermination { get; set; }

        public bool IsDisqualified { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //BoardOfDirectorMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

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
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> CustomerAccountDropdownList
        {
            get
            {
                return customerAccountRepository.CustomerAccountDropdownList;
            }

        }

        public List<SelectListItem> DesignationDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDesignationDropdownList;
            }

        }
    }
}
