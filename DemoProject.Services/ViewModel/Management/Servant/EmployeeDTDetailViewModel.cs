using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeDTDetailViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IWorkingScheduleRepository workingScheduleRepository;

        public EmployeeDTDetailViewModel() 
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            workingScheduleRepository = DependencyResolver.Current.GetService<IWorkingScheduleRepository>();
        }

        // List<SelectListItem> For Dropdownlist

        public List<SelectListItem> DocumentNameDropdownList
        {
            get
            {
                return personDetailRepository.DocumentDropdownList;
            }
        }

        public List<SelectListItem> SalaryBreakupDropdownList
        {
            get
            {
                return managementDetailRepository.SalaryBreakupDropdownList;
            }
        }

        public List<SelectListItem> DepartmentDropdownList
        {
            get
            {
                return managementDetailRepository.DepartmentDropdownList;
            }
        }

        public List<SelectListItem> DesignationDropdownList
        {
            get
            {
                return managementDetailRepository.EmployeeDesignationDropdownList;
            }
        }

        public List<SelectListItem> EmployeeTypeDropdownList
        {
            get
            {
                return managementDetailRepository.EmployeeTypeDropdownList;
            }
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.PersonDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> WorkingScheduleDropdownList
        {
            get
            {
                return workingScheduleRepository.WorkingSchedules;
            }
        }
    }
}
