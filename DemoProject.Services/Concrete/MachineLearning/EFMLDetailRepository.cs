using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DemoProject.Domain.Entities.MachineLearning;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.MachineLearning
{
    public class EFMLDetailRepository : IMLDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFMLDetailRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;

        }

        public IEnumerable<MLChapterPoint> MLChapterPoints
        {
            get { return context.MLChapterPoints; }
        }

        public IEnumerable<MLChapter> MLChapters
        {
            get { return context.MLChapters; }
        }

        public IEnumerable<MLParameterConfig> MLParameterConfigs
        {
            get { return context.MLParameterConfigs; }
        }

        public IEnumerable<MLSubject> MLSubjects
        {
            get { return context.MLSubjects; }
        }

        public IEnumerable<MLWordDefination> MLWordDefinations
        {
            get { return context.MLWordDefinations; }
        }

        public IEnumerable<MLWordDefinationTranslation> MLWordDefinationTranslations
        {
            get { return context.MLWordDefinationTranslations; }
        }

        public IEnumerable<MLWord> MLWords
        {
            get { return context.MLWords; }
        }

        public IEnumerable<MLWordTranslation> MLWordTranslations
        {
            get { return context.MLWordTranslations; }
        }
        
        public int GetWordsPrmKeyByName(string _word)
        {
            return context.MLWords
                            .Where(d => d.Word == _word)
                            .Select(d => d.PrmKey).FirstOrDefault();
        }

        public string GetMLConfigValue(string _pointName, string _parameterName)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetMLConfigValue(@NameOfPoint, @NameOfParameter)", new SqlParameter("@NameOfPoint", _pointName), new SqlParameter("@NameOfParameter", _parameterName)).FirstOrDefault();
        }

        public string TranslateInRegionalLanguage(string _englishText)
        {
            short langPrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            int wordPrmKey = GetWordsPrmKeyByName(_englishText);

            string a = context.MLWordTranslations
                            .Where(wt => wt.MLWordPrmKey == wordPrmKey && wt.LanguagePrmKey == langPrmKey)
                            .Select(wt => wt.WordTranslation).FirstOrDefault();

            if (a == null)
                a = "";

            return a;
        }

        public string TranslateInRegionalLanguage(string _controllerName, string _viewName, string _englishText)
        {
            // Get PrmKey Of Menu
            int menuPrmKey = configurationDetailRepository.GetMenuPrmKey(_controllerName, _viewName);

            // Get PrmKey Of Stored Text
            int dictionaryPrmKey = configurationDetailRepository.GetDictionaryPrmKey(menuPrmKey, _englishText);

            // Get PrmKey Of Regional Language 
            short regionalLanguagePrmKey = 2;

            if (dictionaryPrmKey > 0)
            {
                return context.MLWordTranslations
                                .Where(dt => dt.MLWordPrmKey == dictionaryPrmKey && dt.LanguagePrmKey == regionalLanguagePrmKey)
                                .Select(dt => dt.WordTranslation).FirstOrDefault();
            }
            return _englishText;
        }
    }
}
