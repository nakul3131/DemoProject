using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Master;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class VillageTownCityDetailViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IMLDetailRepository iMLDetailRepository;
        private readonly IDistrictRepository districtRepository;
        private readonly ITalukaRepository talukaRepository;

        public VillageTownCityDetailViewModel()
        {
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            districtRepository = DependencyResolver.Current.GetService<IDistrictRepository>();
            talukaRepository = DependencyResolver.Current.GetService<ITalukaRepository>();
        }

        // Translation In Regional
        [StringLength(100)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

        public List<SelectListItem> CountryDropdownList
        {
            get
            {
                return personDetailRepository.CountryDropdownList;
            }
        }

        public List<SelectListItem> DistrictDropdownListByDivisionId(Guid _divisionId)
        {
            return personDetailRepository.DistrictDropdownListByDivisionId(_divisionId);
        }

        public List<SelectListItem> DivisionDropdownListByStateId(Guid _stateId)
        {
            return personDetailRepository.DivisionDropdownListByStateId(_stateId);
        }

        public List<SelectListItem> OccupationDropdownList
        {
            get
            {
                return personDetailRepository.OccupationDropdownList;
            }
        }

        public List<SelectListItem> PostalOfficeDropdownListByTalukaId(Guid _talukaId)
        {
            return personDetailRepository.PostalOfficeDropdownListByTalukaId(_talukaId);
        }

        public List<SelectListItem> StateDropdownListByCountryId(Guid _countryId)
        {
            return personDetailRepository.StateDropdownListByCountryId(_countryId);
        }

        public List<SelectListItem> SubDivisionOfficeDropdownListByDistrictId(Guid _districtId)
        {
            return personDetailRepository.SubDivisionOfficeDropdownListByDistrictId(_districtId);
        }

        public List<SelectListItem> TalukaDropdownListBySubDivisionOfficeId(Guid _subDivisionOfficeId)
        {
            return personDetailRepository.TalukaDropdownListBySubDivisionOfficeId(_subDivisionOfficeId);
        }

        public List<SelectListItem> TradingEntityDropdownList
        {
            get
            {
                return personDetailRepository.TradingEntityDropdownList;
            }
        }

        public List<SelectListItem> AreaTypeDropdownList
        {
            get
            {
                return personDetailRepository.AreaTypeDropdownList;
            }
        }

        public List<SelectListItem> DirectionDropdownList
        {
            get
            {
                return personDetailRepository.DirectionDropdownList;
            }
        }

        public List<SelectListItem> EducationLevelDropdownList
        {
            get
            {
                return personDetailRepository.EducationLevelDropdownList;
            }
        }

        public List<SelectListItem> FamilySystemDropdownList
        {
            get
            {
                return personDetailRepository.FamilySystemDropdownList;
            }
        }

        public List<SelectListItem> LocalGovernmentDropdownList
        {
            get
            {
                return personDetailRepository.LocalGovernmentDropdownList;
            }
        }
        
        public List<SelectListItem> SubDivisionOfficeDropdownList
        {
            get
            {
                return personDetailRepository.SubDivisionOfficeDropdownList;
            }
        }

        public List<SelectListItem> DistrictDropdownList
        {
            get
            {
                return personDetailRepository.DistrictDropdownList;
            }
        }
    }
}
