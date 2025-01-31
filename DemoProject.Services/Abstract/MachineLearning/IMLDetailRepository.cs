using System.Collections.Generic;
using DemoProject.Domain.Entities.MachineLearning;

namespace DemoProject.Services.Abstract.MachineLearning
{
    public interface IMLDetailRepository
    {

        IEnumerable<MLChapterPoint> MLChapterPoints { get; }

        IEnumerable<MLChapter> MLChapters { get; }

        IEnumerable<MLParameterConfig> MLParameterConfigs { get; }

        IEnumerable<MLSubject> MLSubjects { get; }

        IEnumerable<MLWordDefination> MLWordDefinations { get; }

        IEnumerable<MLWordDefinationTranslation> MLWordDefinationTranslations { get; }

        IEnumerable<MLWord> MLWords { get; }

        IEnumerable<MLWordTranslation> MLWordTranslations { get; }



        int GetWordsPrmKeyByName(string _word);

        string GetMLConfigValue(string _pointName, string _parameterName);

        string TranslateInRegionalLanguage(string _englishText);

        string TranslateInRegionalLanguage(string _controllerName, string _viewName, string _englishText);


    }
}
