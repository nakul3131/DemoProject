using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerDetailRepository : IGeneralLedgerDetailRepository
    {
        private readonly EFDbContext context;

        public EFGeneralLedgerDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public short GetBusinessOfficePrmKeyById(Guid _businessOfficeId)
        {
            return context.BusinessOffices
                        .Where(b => b.BusinessOfficeId == _businessOfficeId)
                        .Select(b => b.PrmKey).FirstOrDefault();
        }

        public bool GetUniqueGLName(string _nameOfGL)
        {
            bool status;
            if (context.GeneralLedgers.Where(p => p.NameOfGL == _nameOfGL).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                // Already registered  
                status = false;
            }
            else
            {
                // Available to use  
                status = true;
            }

            return status;
        }

        public int GetNumberOfGeneralLegerLimit(Guid _accountClassId)
        {
            return context.AccountClasses
                          .Where(a => a.AccountClassId == _accountClassId)
                          .Select(a => a.NumberOfGeneralLedgerLimit).FirstOrDefault();
        }

        public int GetCountOfGeneralLedger()
        {
            var a = context.GeneralLedgers.Where(b => b.ActivationStatus == "ACT" && b.EntryStatus == "VRF").Count();
            return a;
        }

        //GeneralLedgerBusinessOffice
        public async Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<GeneralLedgerBusinessOfficeViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmkey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //GeneralLedgerCurrency
        public async Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<GeneralLedgerCurrencyViewModel>("SELECT * FROM dbo.GetGeneralLedgerCurrencyEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey,@GeneralLedgerPrmkey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //GeneralLedgerScheme
        public async Task<IEnumerable<SchemeGeneralLedgerViewModel>> GetGeneralLedgerSchemeEntries(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<SchemeGeneralLedgerViewModel>("SELECT * FROM dbo.GetGeneralLedgerSchemeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey,@GeneralLedgerPrmkey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //GeneralLedgerTransactionType
        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetGeneralLedgerTransactionTypeEntries(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey,@GeneralLedgerPrmkey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //GeneralLedgerCustomerType
        public async Task<IEnumerable<GeneralLedgerCustomerTypeViewModel>> GetGeneralLedgerCustomerTypeEntries(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<GeneralLedgerCustomerTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerCustomerTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey,@GeneralLedgerPrmkey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        // DropdownList
        public List<SelectListItem> GLParentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from h in context.GeneralLedgers
                            join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()

                            where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    || (h.EntryStatus == StringLiteralValue.Verify)
                                    && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    && (h.IsModified.Equals(false))
                            orderby h.NameOfGL
                            select new SelectListItem
                            {
                                Value = h.GeneralLedgerId.ToString(),
                                Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()) : mf.NameOfGL + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()))
                            }).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from h in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                || (h.EntryStatus == StringLiteralValue.Verify)
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (h.IsModified.Equals(false))
                        orderby h.NameOfGL
                        select new SelectListItem
                        {
                            Value = h.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() : mf.NameOfGL.Trim())
                        }).ToList();
            }
        }

        public List<SelectListItem> GetBusinessOfficeGeneralLedgerDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var F = (from h in context.GeneralLedgers
                             join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                             from mf in hm.DefaultIfEmpty()
                             join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                             from t in ht.DefaultIfEmpty()
                             join a in context.AccountClasses on h.AccountClassPrmKey equals a.PrmKey into ah
                             from a in ah.DefaultIfEmpty()

                             where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                     && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                     && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (a.AccountClassCode == "BSO")
                                     || (h.EntryStatus == StringLiteralValue.Verify)
                                     && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (h.IsModified.Equals(false))
                                    && (a.AccountClassCode == "BSO")
                             orderby h.NameOfGL
                             select new SelectListItem
                             {
                                 Value = h.GeneralLedgerId.ToString(),
                                 Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()) : mf.NameOfGL + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()))
                             }).ToList();
                    return F;
                }

                // Default List In Default Language (i.e. English)
                return (from h in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        join a in context.AccountClasses on h.AccountClassPrmKey equals a.PrmKey into ah
                        from a in ah.DefaultIfEmpty()

                        where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                 && (a.AccountClassCode == "BSO")
                                || (h.EntryStatus == StringLiteralValue.Verify)
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (h.IsModified.Equals(false))
                                 && (a.AccountClassCode == "BSO")
                        orderby h.NameOfGL
                        select new SelectListItem
                        {
                            Value = h.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() : mf.NameOfGL.Trim())
                        }).ToList();
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return (from e in context.GeneralLedgers

                        select new SelectListItem
                        {
                            Value = e.GeneralLedgerId.ToString(),
                            Text = e.NameOfGL
                        }).ToList();
            }
        }

        //Set Default Value For General Ledger 
        public void GetGeneralLedgerAllDefaultValues(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType)
        {
            _generalLedgerViewModel.ActivationStatus = StringLiteralValue.Active;
            _generalLedgerViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _generalLedgerViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerViewModel.EntryStatus = _entryType;
            _generalLedgerViewModel.UserAction = _entryType;
            _generalLedgerViewModel.Remark = "None";

            if (_generalLedgerViewModel.ReasonForModification == null)
            {
                _generalLedgerViewModel.ReasonForModification = "None";
            }
        }

        public void GetGeneralLedgerBusinessOfficeDefaultValues(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string _entryType, short _generalLedgerPrmKey)
        {
            _generalLedgerBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerBusinessOfficeViewModel.EntryStatus = _entryType;
            _generalLedgerBusinessOfficeViewModel.UserAction = _entryType;
            _generalLedgerBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            if (_generalLedgerBusinessOfficeViewModel.Note == null)
            {
                _generalLedgerBusinessOfficeViewModel.Note = "None";
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _generalLedgerBusinessOfficeViewModel.ReasonForModification = "None";
                _generalLedgerBusinessOfficeViewModel.CloseDate = null;
            }

            if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Modify)
            {
                _generalLedgerBusinessOfficeViewModel.Remark = "None";
                _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey = _generalLedgerPrmKey;
            }
        }

        public void GetGeneralLedgerCurrencyDefaultValues(GeneralLedgerCurrencyViewModel _generalLedgerCurrencyViewModel, string _entryType, short _generalLedgerPrmKey)
        {
            _generalLedgerCurrencyViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerCurrencyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerCurrencyViewModel.EntryStatus = _entryType;
            _generalLedgerCurrencyViewModel.UserAction = _entryType;
            _generalLedgerCurrencyViewModel.ActivationStatus = StringLiteralValue.Active;
            if (_generalLedgerCurrencyViewModel.Note == null)
            {
                _generalLedgerCurrencyViewModel.Note = "None";
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _generalLedgerCurrencyViewModel.ReasonForModification = "None";
                _generalLedgerCurrencyViewModel.CloseDate = null;
            }

            if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Modify)
            {
                _generalLedgerCurrencyViewModel.Remark = "None";
                _generalLedgerCurrencyViewModel.GeneralLedgerPrmKey = _generalLedgerPrmKey;
            }
        }

        public void GetGeneralLedgerSchemeDefaultValues(SchemeGeneralLedgerViewModel _generalLedgerSchemeViewModel, string _entryType, short _generalLedgerPrmKey)
        {
            _generalLedgerSchemeViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerSchemeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerSchemeViewModel.EntryStatus = _entryType;
            _generalLedgerSchemeViewModel.UserAction = _entryType;
            _generalLedgerSchemeViewModel.ActivationStatus = StringLiteralValue.Active;
            if (_generalLedgerSchemeViewModel.Note == null)
            {
                _generalLedgerSchemeViewModel.Note = "None";
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _generalLedgerSchemeViewModel.ReasonForModification = "None";
                _generalLedgerSchemeViewModel.CloseDate = null;
            }

            if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Modify)
            {
                _generalLedgerSchemeViewModel.Remark = "None";
                _generalLedgerSchemeViewModel.GeneralLedgerPrmKey = _generalLedgerPrmKey;
            }
        }

        public void GetGeneralLedgerTransactionTypeDefaultValues(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel, string _entryType, short _generalLedgerPrmKey)
        {
            _generalLedgerTransactionTypeViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerTransactionTypeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerTransactionTypeViewModel.EntryStatus = _entryType;
            _generalLedgerTransactionTypeViewModel.UserAction = _entryType;
            _generalLedgerTransactionTypeViewModel.ActivationStatus = StringLiteralValue.Active;

            if (_generalLedgerTransactionTypeViewModel.Note == null)
            {
                _generalLedgerTransactionTypeViewModel.Note = "None";
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _generalLedgerTransactionTypeViewModel.ReasonForModification = "None";
                _generalLedgerTransactionTypeViewModel.CloseDate = null;
            }

            if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Modify)
            {
                _generalLedgerTransactionTypeViewModel.Remark = "None";
                _generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey = _generalLedgerPrmKey;
            }
        }

        public void GetGeneralLedgerCustomerTypeDefaultValues(GeneralLedgerCustomerTypeViewModel _generalLedgerCustomerTypeViewModel, string _entryType, short _generalLedgerPrmKey)
        {
            _generalLedgerCustomerTypeViewModel.EntryDateTime = DateTime.Now;
            _generalLedgerCustomerTypeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _generalLedgerCustomerTypeViewModel.EntryStatus = _entryType;
            _generalLedgerCustomerTypeViewModel.UserAction = _entryType;
            _generalLedgerCustomerTypeViewModel.ActivationStatus = StringLiteralValue.Active;

            if (_generalLedgerCustomerTypeViewModel.Note == null)
            {
                _generalLedgerCustomerTypeViewModel.Note = "None";
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _generalLedgerCustomerTypeViewModel.ReasonForModification = "None";
                _generalLedgerCustomerTypeViewModel.CloseDate = null;
            }

            if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Modify)
            {
                _generalLedgerCustomerTypeViewModel.Remark = "None";
                _generalLedgerCustomerTypeViewModel.GeneralLedgerPrmKey = _generalLedgerPrmKey;
            }
        }
    }
}
