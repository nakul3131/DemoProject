using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Master;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class BeneficiaryDTDetailViewModel
    {
        private readonly IBeneficiaryRepository beneficiaryRepository;

        public BeneficiaryDTDetailViewModel() 
        {
            beneficiaryRepository = DependencyResolver.Current.GetService<IBeneficiaryRepository>();
        }

        public List<SelectListItem> CustomerAccountTypeDropdownList
        {
            get
            {
                return beneficiaryRepository.CustomerAccountTypeDropdownList;
            }
        }
    }
}
