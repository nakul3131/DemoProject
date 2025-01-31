using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.AppSupported;

namespace DemoProject.Services.Concrete.AppSupported
{
    public class EFAppSupportedLanguageRepository : IAppSupportedLanguageRepository
    {
        private readonly EFDbContext context;
        //private readonly IMLWordTranslationRepository wordTranslationRepository;

        public EFAppSupportedLanguageRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
            //wordTranslationRepository = _wordTranslationRepository;
        }

        public string GetNameOfLanguage(short _languagePrmKey)
        {
            return context.Languages
                    .Where(l => l.PrmKey == _languagePrmKey)
                    .Select(l => l.NameOfLanguage).FirstOrDefault();
        }

        //public string GetNameOfRegionalLanguage(short _languagePrmKey)
        //{
        //    string nameOfLanguage = GetNameOfLanguage(_languagePrmKey);

        //    return wordTranslationRepository.TranslateInRegionalLanguage(nameOfLanguage);
        //}

        public short GetPrmKeyById(Guid _appSupportedLanguageId)
        {
            return context.Languages
                    .Where(c => c.LanguageId == _appSupportedLanguageId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> AppSupportedLanguageDropdownList


        {
            get

            {
                return (from e in context.Languages
                        //join d in context.AppSupportedLanguages on e.PrmKey equals d.LanguagePrmKey
                        select new SelectListItem
                        {
                            Value = e.LanguageId.ToString(),
                            Text = e.NameOfLanguage,
                            Selected = true,
                        }).ToList();
            }
        }
    }
}
