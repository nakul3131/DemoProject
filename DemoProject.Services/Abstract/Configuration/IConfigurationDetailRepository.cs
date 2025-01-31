using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Services.ViewModel.Configuration;

namespace DemoProject.Services.Abstract.Configuration
{
    public interface IConfigurationDetailRepository
    {
        IEnumerable<AppConfig> AppConfigs { get; }

        IEnumerable<MenuSearchQueryResultViewModel> GetMenuSearchQueryResultBySearchQueryId(Guid _searchQueryId);

        IEnumerable<SearchQueryViewModel> GetSearchQueryList(string _inputString);

        IEnumerable<Menu> Menus { get; }

        List<SelectListItem> AppDataTypes { get; }

        List<SelectListItem> AppLanguageDropdownList { get; }

        List<SelectListItem> FileFormatTypes { get; }

        List<SelectListItem> LanguageDropdownList { get; }

        List<SelectListItem> GetReportTypeFormatEntries(Guid _reportTypeId);

        List<SelectListItem> GetReportTypeFormatDropdownListForEdit(Guid _reportTypeId);

        List<SelectListItem> MaskTypeDropdownList { get; }

        List<SelectListItem> ModelMenuDropdownList { get; }

        List<SelectListItem> ReportTypeFormatDropdownList { get; }

        List<SelectListItem> ReportTypeDropdownList { get; }

        List<SelectListItem> TimePeriodUnitDropdownList { get; }


        void SetDefaultValues(object _obj, string _entryType);

        string TranslateNumberInRegionalLanguage(decimal Number, int LanguagePrmkey);

        List<string> NumberInWordsDropdownList(int LanguagePrmKey);

        short GetAppLanguagePrmKeyById(Guid _appLanguageId);

        byte GetDataTypePrmKeyById(Guid _AreaTypeId);

        short GetLanguagePrmKeyById(Guid _languageId);

        byte GetMaskTypePrmKeyById(Guid _maskTypeId);

        byte GetMonthlyTimePeriodUnitPrmKeyById();

        int GetMenuPrmKey(string _controllerName, string _viewName);

        int GetReportTypeFormatPrmKeyById(Guid _reportTypeFormatId);

        int GetReportTypePrmKeyById(Guid _reportTypeId);

        int GetSearchQueryPrmKeyById(Guid _searchQueryId);

        byte GetTimePeriodUnitPrmKeyById(Guid _timePeriodUnitId);

        byte GetNumberOfBranches();

        int GetDictionaryPrmKey(int _menuPrmKey, string _labelName);

        int GetMenuPrmKeyById(Guid _menuId);

        string GetBranchTypeById(Guid homeBranchId);

        string GetMenuPath(string _menuCode);

        string GetNameOfLanguage(short _languagePrmKey);

        string GetSysNameOfTimePeriodUnitById(byte _timePeriodUnitPrmKey);

        string GetNameOfRegionalLanguage(short _languagePrmKey);

        string GetNameInGivenLanguage(short _pageTableFieldPrmKey, short _languagePrmKey);

        string GetNameOfMenuByMenuId(string _menuCode);

        Menu GetUrl(int _prmKey);

        short GetDefaultCurrencyPrmKey();

        bool IsRegisteredUnderCooperative();

        bool IsRegisteredUnderRBI();

        bool IsActiveSoftwareSubscriptionStauts();

        bool HasCoreBankingFeature();

        bool HasMultiCurrency();

        bool IsEnabledSmsService();

        bool IsEnabledEmailService();

        int GetAge(DateTime _bithDate);

        int GetAge(DateTime _fromDate, DateTime _bithDate);
    }
}
