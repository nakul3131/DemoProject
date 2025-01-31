using DemoProject.Services.Abstract.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Configuration
{
    public  class TranslatorViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;

        public TranslatorViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        public string TranslateInRegionalLanguage(string _englishText)
        {
            return mlDetailRepository.TranslateInRegionalLanguage(_englishText);
        }
    }
}
