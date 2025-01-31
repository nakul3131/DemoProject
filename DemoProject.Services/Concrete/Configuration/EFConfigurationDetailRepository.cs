using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Configuration
{
    public class EFConfigurationDetailRepository : IConfigurationDetailRepository
    {
        private readonly EFDbContext context;
        //private readonly IMLDetailRepository mlDetailRepository;

        public EFConfigurationDetailRepository(RepositoryConnection _connection/*, IMLDetailRepository _mlDetailRepository*/)
        {
            context = _connection.EFDbContext;
            //mlDetailRepository = _mlDetailRepository;
        }

        public IEnumerable<AppConfig> AppConfigs
        {
            get
            {
                return context.AppConfigs;                       
            }
        }

        public IEnumerable<MenuSearchQueryResultViewModel> GetMenuSearchQueryResultBySearchQueryId(Guid _searchQueryId)
        {
            int searchQueryPrmKey = GetSearchQueryPrmKeyById(_searchQueryId);

            return context.Database.SqlQuery<MenuSearchQueryResultViewModel>("SELECT * FROM dbo.GetMenuSearchQueryResult (@SearchQueryPrmKey, @UserProfilePrmKey)", new SqlParameter("@SearchQueryPrmKey", searchQueryPrmKey), new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"])).ToList();
        }

        public IEnumerable<SearchQueryViewModel> GetSearchQueryList(string _inputString)
        {
            return (from s in context.SearchQueries
                    where s.QueryText.ToLower().Contains(_inputString.ToLower())
                    select new SearchQueryViewModel { QueryText = s.QueryText, SearchQueryId = s.SearchQueryId }).ToList();
        }

        public IEnumerable<Menu> Menus
        {
            get { return context.Menus; }
        }

        public List<SelectListItem> AppDataTypes
        {
            get
            {
                return (from e in context.AppDataTypes

                        select new SelectListItem
                        {
                            Value = e.DataTypeId.ToString(),
                            Text = e.NameOfDataType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AppLanguageDropdownList
        {
            get
            {
                return (from e in context.AppLanguages

                        select new SelectListItem
                        {
                            Value = e.AppLanguageId.ToString(),
                            Text = e.NameOfAppLanguage
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FileFormatTypes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text=".none",
                        Value = "none"
                    },
                    new SelectListItem
                    {
                        Text=".jpg",
                        Value = "jpg"
                    },
                    new SelectListItem
                    {
                        Text=".jpeg",
                        Value = "jpeg"
                    },
                    new SelectListItem
                    {
                        Text=".png",
                        Value = "png"
                    },
                    new SelectListItem
                    {
                        Text=".pdf",
                        Value = "pdf"
                    },
                };
            }
        }

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return (from e in context.Languages

                        select new SelectListItem
                        {
                            Value = e.LanguageId.ToString(),
                            Text = e.NameOfLanguage
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GetReportTypeFormatEntries(Guid _reportTypeId)
        {
            int reportTypePrmKey = GetReportTypePrmKeyById(_reportTypeId);

            return (from m in context.Menus
                    join rt in context.ReportTypeFormats on m.PrmKey equals rt.MenuPrmKey into reportTypeFormat
                    from rt in reportTypeFormat.DefaultIfEmpty()
                    where ((from f in context.ReportTypeFormats
                            where f.ReportTypePrmKey == reportTypePrmKey
                            select f.MenuPrmKey).Contains(m.PrmKey)
                           )
                    select new SelectListItem
                    {
                        Value = rt.ReportTypeFormatId.ToString(),
                        Text = m.NameOfMenu
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetReportTypeFormatDropdownListForEdit(Guid _reportTypeId)
        {
            int reportTypePrmKey = GetReportTypePrmKeyById(_reportTypeId);

            return (from m in context.Menus
                     join rt in context.ReportTypeFormats on m.PrmKey equals rt.MenuPrmKey into reportTypeFormat
                     from rt in reportTypeFormat.DefaultIfEmpty()
                     where ((from f in context.ReportTypeFormats
                             where f.ReportTypePrmKey == reportTypePrmKey
                             select f.MenuPrmKey).Contains(m.PrmKey)
                            )
                     select new SelectListItem
                     {
                         Value = rt.ReportTypeFormatId.ToString(),
                         Text = m.NameOfMenu
                     }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> MaskTypeDropdownList
        {
            get
            {
                // Default List In Default Language (i.e. English)
                return (from m in context.MaskTypes
                        where (m.ActivationStatus == StringLiteralValue.Active)
                        orderby m.NameOfMaskType
                        select new SelectListItem
                        {
                            Value = m.MaskCharacter.ToString(),
                            Text = m.NameOfMaskType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public byte GetMonthlyTimePeriodUnitPrmKeyById()
        {
            return context.TimePeriodUnits
                    .Where(c => c.SysNameOfUnit == "Month")
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> ModelMenuDropdownList
        {
            get
            {
                return (from a in context.Menus
                        where (a.ParentMenuPrmKey == 0)
                        select new SelectListItem
                        {
                            Value = a.MenuId.ToString(),
                            Text = a.NameOfMenu
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ReportTypeFormatDropdownList
        {
            get
            {

                return (from m in context.Menus
                        join rt in context.ReportTypeFormats on m.PrmKey equals rt.MenuPrmKey into reportTypeFormat
                        from rt in reportTypeFormat.DefaultIfEmpty()
                        where ((from f in context.ReportTypeFormats
                                select f.MenuPrmKey).Contains(m.PrmKey)
                               )
                        orderby m.NameOfMenu
                        select new SelectListItem
                        {
                            Value = rt.ReportTypeFormatId.ToString(),
                            Text = m.NameOfMenu
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ReportTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    IList<string> ReportTypeFormatList = (from s in context.ReportTypes
                                                          group s by s.PrmKey into g
                                                          join sh in context.ReportTypeFormats on g.FirstOrDefault().PrmKey equals sh.ReportTypePrmKey
                                                          select g.FirstOrDefault().ReportTypeId.ToString()).ToList();

                    // Get All Valid Selectlist From VehicleVariants            
                    IList<SelectListItem> reportTypeDropdownList = (from r in context.ReportTypes
                                                                    join t in context.ReportTypeTranslations on r.PrmKey equals t.ReportTypePrmKey into rt
                                                                    from t in rt.DefaultIfEmpty()
                                                                    where (r.ActivationStatus == StringLiteralValue.Active)
                                                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                                                    select new SelectListItem
                                                                    {
                                                                        Value = r.ReportTypeId.ToString(),
                                                                        Text = (r.NameOfReportType.Trim() + " ---> " + (t.TransNameOfReportType.Trim() ?? " " ))
                                                                    }).Distinct().OrderBy(l => l.Text).ToList();

                    // Get Final DropdownList
                    return (from s in reportTypeDropdownList
                            where (ReportTypeFormatList).Contains(s.Value)
                            select new SelectListItem
                            {
                                Value = s.Value,
                                Text = s.Text
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from r in context.ReportTypes
                        where (r.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = r.ReportTypeId.ToString(),
                            Text = r.NameOfReportType.Trim()
                        }).ToList();
            }
        }

        public List<SelectListItem> TimePeriodUnitDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from u in context.TimePeriodUnits
                            join t in context.TimePeriodUnitTranslations on u.PrmKey equals t.TimePeriodUnitPrmKey into ut
                            from t in ut.DefaultIfEmpty()
                            where (u.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = u.TimePeriodUnitId.ToString(),
                                Text = (u.NameOfUnit.Trim() + " ---> " + (t.TransNameOfUnit.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).Distinct().OrderBy(l => l.Text).ToList();

                }

                return (from u in context.TimePeriodUnits
                        where (u.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = u.TimePeriodUnitId.ToString(),
                            Text = u.NameOfUnit
                        }).Distinct().OrderBy(l=>l.Text).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public void SetDefaultValues(object _obj, string _entryType)
        {
            byte bytePrmKey = 0;
            short shortPrmKey = 0;
            int intPrmKey = 0;
            long longPrmKey = 0;

            string[] objectFullNameArray = _obj.ToString().Split('.');

            string viewModelName = objectFullNameArray[objectFullNameArray.Length - 1];

            PropertyInfo[] viewModelProperties = _obj.GetType().GetProperties();

            // PrmKey 
            PropertyInfo propPrmKey = _obj.GetType().GetProperty("PrmKey");

            //  For Other Property 
            // EntryDateTime 
            PropertyInfo prop = _obj.GetType().GetProperty("EntryDateTime");

            if (prop != null)
                prop.SetValue(_obj, DateTime.Now, null);

            // ActivationStatus 
            prop = _obj.GetType().GetProperty("ActivationStatus");

            if (prop != null)
                prop.SetValue(_obj, StringLiteralValue.Active, null);

            // EntryStatus 
            prop = _obj.GetType().GetProperty("EntryStatus");

            if (prop != null)
                prop.SetValue(_obj, _entryType, null);

            // UserAction 
            prop = _obj.GetType().GetProperty("UserAction");

            if (prop != null)
                prop.SetValue(_obj, _entryType, null);

            // UserProfilePrmKey 
            prop = _obj.GetType().GetProperty("UserProfilePrmKey");

            if (prop != null)
                prop.SetValue(_obj, (short)HttpContext.Current.Session["UserProfilePrmKey"], null);

            // LanguagePrmKey 
            prop = _obj.GetType().GetProperty("LanguagePrmKey");

            if (prop != null)
                prop.SetValue(_obj, (short)HttpContext.Current.Session["RegionalLanguagePrmKey"], null);

            // Note 
            prop = _obj.GetType().GetProperty("Note");

            if (prop != null)
            {
                string note = (string)prop.GetValue(_obj);

                if (note is null)
                    note = "None";

                prop.SetValue(_obj, note, null);
            }

            // Reason For Modification 
            prop = _obj.GetType().GetProperty("ReasonForModification");

            if (prop != null)
            {
                string reasonForModification = (string)prop.GetValue(_obj);

                if (reasonForModification is null)
                    reasonForModification = "None";

                prop.SetValue(_obj, reasonForModification, null);
            }

            // Remark 
            prop = _obj.GetType().GetProperty("Remark");

            if (prop != null)
            {
                string remark = (string)prop.GetValue(_obj);

                if (remark is null)
                    remark = "None";

                prop.SetValue(_obj, remark, null);
            }

            // ViewModelPrmKey 
            prop = _obj.GetType().GetProperty(viewModelName.Replace("ViewModel", "") + "PrmKey");

            if (prop != null && propPrmKey != null)
            {
                if (prop.PropertyType == typeof(byte))
                {
                    if (_entryType != StringLiteralValue.Create)
                    {
                        bytePrmKey = (byte)(prop.GetValue(_obj));
                        propPrmKey.SetValue(_obj, bytePrmKey, null);
                    }
                    else
                    {
                        // Set PrmKey And ViewModel PrmKey = 0
                        bytePrmKey = 0;
                        propPrmKey.SetValue(_obj, bytePrmKey);
                        prop.SetValue(_obj, bytePrmKey);
                    }
                }

                if (prop.PropertyType == typeof(short))
                {
                    if (_entryType != StringLiteralValue.Create)
                    {
                        shortPrmKey = (short)(prop.GetValue(_obj));
                        propPrmKey.SetValue(_obj, shortPrmKey, null);
                    }
                    else
                    {
                        // Set PrmKey And ViewModel PrmKey = 0
                        shortPrmKey = 0;
                        propPrmKey.SetValue(_obj, shortPrmKey);
                        prop.SetValue(_obj, shortPrmKey);
                    }
                }

                if (prop.PropertyType == typeof(int))
                {
                    if (_entryType != StringLiteralValue.Create)
                    {
                        intPrmKey = (int)(prop.GetValue(_obj));
                        propPrmKey.SetValue(_obj, intPrmKey, null);
                    }
                    else
                    {
                        // Set PrmKey And ViewModel PrmKey = 0
                        intPrmKey = 0;
                        propPrmKey.SetValue(_obj, intPrmKey);
                        prop.SetValue(_obj, intPrmKey);
                    }
                }

                if (prop.PropertyType == typeof(long))
                {
                    if (_entryType != StringLiteralValue.Create)
                    {
                        longPrmKey = (long)(prop.GetValue(_obj));
                        propPrmKey.SetValue(_obj, longPrmKey, null);
                    }
                    else
                    {
                        // Set PrmKey And ViewModel PrmKey = 0
                        longPrmKey = 0;
                        propPrmKey.SetValue(_obj, longPrmKey);
                        prop.SetValue(_obj, longPrmKey);
                    }
                }
            }
        }

        public string TranslateNumberInRegionalLanguage(decimal Number, int LanguagePrmkey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.TranslateNumberInRegionalLanguage(@Number,@LanguagePrmkey)", new SqlParameter("@LanguagePrmkey", LanguagePrmkey), new SqlParameter("@Number", Number)).FirstOrDefault();
        }

        public List<string> NumberInWordsDropdownList(int LanguagePrmKey)
        {
            return context.NumberInWords
                      .Where(c => c.LanguagePrmKey == LanguagePrmKey)
                      .Select(c => new List<string> { c.HigherDigitNumberWords, c.HundredDigitNumberWords, c.NumberSeparatorWords }).FirstOrDefault();
        }

        public short GetAppLanguagePrmKeyById(Guid _appLanguageId)
        {
            return context.AppLanguages
                    .Where(c => c.AppLanguageId == _appLanguageId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetDataTypePrmKeyById(Guid _dataTypeId)
        {
            return context.AppDataTypes
                   .Where(c => c.DataTypeId == _dataTypeId)
                   .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetLanguagePrmKeyById(Guid _languageId)
        {
            return context.Languages
                    .Where(c => c.LanguageId == _languageId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetMaskTypePrmKeyById(Guid _maskTypeId)
        {
            return context.MaskTypes
                    .Where(d => d.MaskTypeId == _maskTypeId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public int GetMenuPrmKeyById(Guid _menuId)
        {
            return context.Menus
                    .Where(c => c.MenuId == _menuId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public string GetBranchTypeById(Guid _homeBranchId)
        {
            var a = (from b in context.BusinessOffices
                     join bd in context.BusinessOfficeDetails on b.PrmKey equals bd.BusinessOfficePrmKey
                     join bt in context.BusinessOfficeTypes on bd.BusinessOfficeTypePrmKey equals bt.PrmKey
                     where (b.BusinessOfficeId == _homeBranchId)
                     select bt.SysNameOfBusinessOffice).FirstOrDefault();
            return a;
        }

        public int GetReportTypeFormatPrmKeyById(Guid _reportTypeFormatId)
        {
            return context.ReportTypeFormats
                    .Where(c => c.ReportTypeFormatId == _reportTypeFormatId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public int GetReportTypePrmKeyById(Guid _reportTypeId)
        {
            return context.ReportTypes
                    .Where(c => c.ReportTypeId == _reportTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public int GetSearchQueryPrmKeyById(Guid _searchQueryId)
        {
            return context.SearchQueries
                    .Where(s => s.SearchQueryId == _searchQueryId)
                    .Select(s => s.PrmKey).FirstOrDefault();
        }

        public byte GetTimePeriodUnitPrmKeyById(Guid _timePeriodUnitId)
        {
            return context.TimePeriodUnits
                    .Where(c => c.TimePeriodUnitId == _timePeriodUnitId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetNumberOfBranches()
        {
            return context.AppConfigs
                    .Select(a => a.NumberOfBranch).FirstOrDefault();
        }

        public int GetDictionaryPrmKey(int _menuPrmKey, string _labelName)
        {
            return (from d in context.MLWords
                    join ml in context.MenuLabels on d.PrmKey equals ml.MLWordPrmKey
                    where (d.Word == _labelName) && (ml.MenuPrmKey == _menuPrmKey)
                    select ml.MLWordPrmKey).FirstOrDefault();
        }

        public int GetMenuPrmKey(string _controllerName, string _viewName)
        {
            return context.Menus
                            .Where(m => m.NameOfActionMethod == _viewName & m.NameOfController == _controllerName)
                            .Select(m => m.PrmKey).FirstOrDefault();
        }

        public string GetMenuPath(string _menuCode)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetMenuPath(@NameOfMenu)", new SqlParameter("@NameOfMenu", _menuCode)).FirstOrDefault();
        }

        public string GetNameInGivenLanguage(short _pageTableFieldPrmKey, short _languagePrmKey)
        {
            return context.PageTableFieldTranslations
                            .Where(p => p.PageTableFieldPrmKey == _pageTableFieldPrmKey & p.LanguagePrmKey == _languagePrmKey)
                            .Select(p => p.NameOfPageTableField).FirstOrDefault();
        }

        public string GetNameOfLanguage(short _languagePrmKey)
        {
            return context.Languages
                    .Where(l => l.PrmKey == _languagePrmKey)
                    .Select(l => l.NameOfLanguage).FirstOrDefault();
        }

        public string GetSysNameOfTimePeriodUnitById(byte _timePeriodUnitPrmKey)
        {
            return context.TimePeriodUnits
                    .Where(l => l.PrmKey == _timePeriodUnitPrmKey)
                    .Select(l => l.SysNameOfUnit).FirstOrDefault();
        }

        public string GetNameOfRegionalLanguage(short _languagePrmKey)
        {
            string nameOfLanguage = GetNameOfLanguage(_languagePrmKey);

            return nameOfLanguage;
                //mlDetailRepository.TranslateInRegionalLanguage(nameOfLanguage);
        }

        public string GetNameOfMenuByMenuId(string _menuCode)
        {
            return context.Menus
                        .Where(m => m.MenuCode == _menuCode)
                        .Select(m => m.NameOfMenu).FirstOrDefault();
        }

        public Menu GetUrl(int _prmKey)
        {
            return context.Menus.Where(m => m.PrmKey == _prmKey).FirstOrDefault();
        }

        public short GetDefaultCurrencyPrmKey()
        {
            return context.AppConfigs
                .Select(a => a.DefaultCurrencyPrmKey).FirstOrDefault();
        }

        public bool IsRegisteredUnderCooperative()
        {
            return context.AppConfigs
                           .Select(a => a.EnableCooperativeRegistration).FirstOrDefault();
        }

        public bool IsRegisteredUnderRBI()
        {
            return context.AppConfigs
                           .Select(a => a.EnableRBIRegistration).FirstOrDefault();
        }

        public bool IsActiveSoftwareSubscriptionStauts()
        {
            return context.Database.SqlQuery<bool>("SELECT dbo.GetSoftwareSubscriptionStatus(@BranchPrmKey)", new SqlParameter("@BranchPrmKey", HttpContext.Current.Session["UserHomeBranchPrmKey"])).FirstOrDefault();
        }

        public bool HasCoreBankingFeature()
        {
            return context.AppConfigs
                        .Select(a=> a.EnableCoreBanking)
                        .FirstOrDefault();
        }

        public bool HasMultiCurrency()
        {
            return context.AppConfigs
                        .Select(a => a.EnableMultiCurrency)
                        .FirstOrDefault();
        }

        public bool IsEnabledSmsService()
        {
            return context.AppConfigs
                        .Select(a => a.EnableSmsService)
                        .FirstOrDefault();
        }

        public bool IsEnabledEmailService()
        {
            return context.AppConfigs
                        .Select(a => a.EnableEmailService)
                        .FirstOrDefault();
        }
        
        public int GetAge(DateTime _bithDate)
        {
            int age = 0;

            DateTime now = DateTime.Now;

            if (_bithDate < now)
            {
                age = now.Year - _bithDate.Year;

                if (now.DayOfYear < _bithDate.DayOfYear)
                {
                    age = age - 1;
                }
            }

            return age;
        }

        public int GetAge(DateTime _fromDate, DateTime _bithDate)
        {
            int age = 0;

            if (_bithDate < _fromDate)
            {
                age = _fromDate.Year - _bithDate.Year;

                if (_fromDate.DayOfYear < _bithDate.DayOfYear)
                {
                    age = age - 1;
                }
            }

            return age;
        }
    }
}
