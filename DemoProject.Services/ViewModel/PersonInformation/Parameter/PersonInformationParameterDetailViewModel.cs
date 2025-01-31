using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation.Parameter
{
    public class PersonInformationParameterDetailViewModel
    {
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public PersonInformationParameterDetailViewModel()
        {
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        public List<SelectListItem> ChecksumAlgorithmDropdownList
        {
            get
            {
                return securityDetailRepository.ChecksumAlgorithmDropdownList;
            }
        }

        public List<SelectListItem> FileFormatTypes
        {
            get
            {
                return configurationDetailRepository.FileFormatTypes;
            }
        }

        public List<SelectListItem> MaskTypeDropdownList
        {
            get
            {
                return configurationDetailRepository.MaskTypeDropdownList;
            }
        }

        // List<SelectListItem> For Dropdown
        public List<SelectListItem> DocumentTypeDropdownList
        {
            get
            {
                return personDetailRepository.DocumentTypeDropdownList;
            }
        }
        public List<SelectListItem> PersonDocumentTypeDropdownList
        {
            get
            {
                return personDetailRepository.PersonDocumentDropdownList;
            }
        }

        public List<SelectListItem> NoticeTypeDropdownList
        {
            get
            {
                return managementDetailRepository.NoticeTypeDropdownList;
            }
        }

        public List<SelectListItem> DataTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>{
                new SelectListItem{
                    Text="Numeric",
                    Value = "NUM"
                },
                  new SelectListItem{
                    Text="Upper Case Alphabet",
                    Value = "UAL"
                },
                  new SelectListItem{
                    Text="Lower Case Alphabet",
                    Value = "LAL"
                },
                  new SelectListItem{
                    Text="AlphaNumeric",
                    Value = "ALN"
                }
            };
            }
        }

    }
}
