using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DemoProject.Services.Abstract.AppSupported
{
    public interface IAppSupportedLanguageRepository
    {
        // Get Name Of Language By PrmKey
        string GetNameOfLanguage(short _languagePrmKey);

        // Get Name Of Regional Language By PrmKey
        //string GetNameOfRegionalLanguage(short _languagePrmKey);

        // Get PrmKey By Id
        short GetPrmKeyById(Guid _languageId);

        // Language Select List For Dropdown
        List<SelectListItem> AppSupportedLanguageDropdownList { get; }
    }
}
