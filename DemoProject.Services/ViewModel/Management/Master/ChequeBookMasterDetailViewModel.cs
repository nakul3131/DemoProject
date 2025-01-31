using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ChequeBookMasterDetailViewModel
    {
        private readonly IMLDetailRepository iMLDetailRepository;
       // private readonly IConfigurationDetailRepository configurationDetailRepository;

        public ChequeBookMasterDetailViewModel()
        {
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            //configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        // Translation In Regional


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

        //public List<SelectListItem> ChequeBookMasterDropdownList
        //{
        //    get
        //    {
        //        return configurationDetailRepository.ChequeBookMasterDropdownList;
        //    }
        //}
    }
}
