using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFTransactionParameterRepository : ITransactionParameterRepository
    {
        private readonly EFDbContext context;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;

        public EFTransactionParameterRepository(RepositoryConnection _connection, ISecurityDetailRepository _securityDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            securityDetailRepository = _securityDetailRepository;
            accountDetailRepository = _accountDetailRepository;
        }

        public List<SelectListItem> GetGLAndAccountNumber(Guid _PersonId)
        {
            long personPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_PersonId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var a = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()

                         join cd1 in context.GeneralLedgers on cd.GeneralLedgerPrmKey equals cd1.PrmKey into cad1
                         from cd1 in cad1.DefaultIfEmpty()

                         where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                         || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                         && (c.IsModified.Equals(false))

                         orderby c.AccountNumber
                         select new SelectListItem
                         {
                            // Value = cd.CustomerAccountDetailId.ToString(),
                             Text = cd1.NameOfGL.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                         }).ToList();
                return a;
            }

            var b = (from c in context.CustomerAccounts
                     join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                     from cd in cad.DefaultIfEmpty()
                     where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))

                     || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))
                     && (c.IsModified.Equals(false))

                     orderby c.AccountNumber
                     select new SelectListItem
                     {
                         Value = c.CustomerAccountId.ToString(),
                         Text = c.AccountNumber.ToString()
                     }).ToList();
            return b;
        }

        public int GetSchemeId(Guid _accountno)
        {

            var a = (from c in context.CustomerAccounts
                     join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                     from cd in cad.DefaultIfEmpty()
                     where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (c.CustomerAccountId.Equals(_accountno))
                     select cd.SchemePrmKey).FirstOrDefault();

            return a;
        }

        public List<SelectListItem> GetAccountNumber(Guid _PersonId)
        {
            long personPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_PersonId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var a = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()

                         where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                         || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                         && (c.IsModified.Equals(false))

                         orderby c.AccountNumber
                         select new SelectListItem
                         {
                             Value = c.CustomerAccountId.ToString(),
                             Text = c.AccountNumber.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                         }).ToList();
                return a;
            }

            var b = (from c in context.CustomerAccounts
                     join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                     from cd in cad.DefaultIfEmpty()
                     where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))

                     || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))
                     && (c.IsModified.Equals(false))

                     orderby c.AccountNumber
                     select new SelectListItem
                     {
                         Value = c.CustomerAccountId.ToString(),
                         Text = c.AccountNumber.ToString()
                     }).ToList();
            return b;
        }

        public IEnumerable<string> GetAccountByhand()
        {

            var data = context.People.Select(p=>p.FullName).ToList();
            //string[] names = { "Peter", "Paul", "Mary" };
            //List<string> result = new List<string>();
            //List<string> optionList = new List<string>();
            //optionList.Add("Pradip Ghanwat");
            //optionList.Add("Ravi Nale");
            //optionList.Add("jay kale");
            //optionList.Add("ajay shinde");
            //optionList.Add("anil shinde");
            //optionList.Add("kumar shinde");
            //optionList.Add("viraj shinde");
            //result = names.AsEnumerable().Select(x => string.Format("{0}-{1}-{2}", x,x,x)).ToArray().ToList();

            return data;

        }

        public List<string> GetPersonAutoCompleteList(string _inputString)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            List<string> result = new List<string>();

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var search = (from p in context.People
                              join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                              from mf in pm.DefaultIfEmpty()
                              join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                              from t in pt.DefaultIfEmpty()
                              join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                              from c in pc.DefaultIfEmpty()
                              join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                              from k in pk.DefaultIfEmpty()
                              where (p.EntryStatus.Equals(StringLiteralValue.Verify) && p.ActivationStatus.Equals(StringLiteralValue.Active) && t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (p.FullName.Contains(_inputString) || t.TransFullName.Contains(_inputString)/* || p.PersonInformationNumber.Contains(_inputString)*/ || p.DateOfBirth.ToString().Contains(_inputString)
                                          || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                              orderby p.FullName
                              select new
                              {
                                  p.FullName,
                                  t.TransFullName,
                                  p.DateOfBirth,
                                  p.PersonInformationNumber,
                                  p.PersonId,
                                  c.FieldValue,
                                  k.DocumentNumber
                              }).ToList();
                var list = search.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, TransFullName1 = l.FirstOrDefault().TransFullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonId1 = l.FirstOrDefault().PersonId, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber }).ToList();
                result = list.AsEnumerable().Select(x => string.Format("{0},{1},{2},{3},{4},{5}", x.FullName1, x.TransFullName1, x.DateOfBirth1,x.PersonId1, x.PersonInformationNumber1, x.FieldValue1, x.DocumentNumber1)).ToList();
                return result;
            }

            // Default List In Default Language (i.e. English)
            var search1 = (from p in context.People
                           join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                           from mf in pm.DefaultIfEmpty()
                           join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                           from c in pc.DefaultIfEmpty()
                           join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                           from k in pk.DefaultIfEmpty()
                           where (p.EntryStatus.Equals(StringLiteralValue.Verify) && p.ActivationStatus.Equals(StringLiteralValue.Active))
                                  && (p.FullName.Contains(_inputString) /*|| p.PersonInformationNumber.Contains(_inputString) */|| p.DateOfBirth.ToString().Contains(_inputString)
                                       || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                           orderby p.FullName

                           select new
                           {
                               p.FullName,
                               p.DateOfBirth,
                               p.PersonInformationNumber,
                               p.PersonId,
                               c.FieldValue,
                               k.DocumentNumber
                           }).ToList();

            var list1 = search1.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, PersonId1 = l.FirstOrDefault().PersonId, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber.Count() }).ToList();
            result = list1.Select(x => string.Format("{0}-{1}-{2}-{3}-{4}-{5}", x.FullName1, x.DateOfBirth1, x.PersonInformationNumber1, x.PersonId1, x.FieldValue1, x.DocumentNumber1)).ToArray().ToList();
            return result;
        }

        public List<string> GetPersonAutoCompleteList()
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            List<string> result = new List<string>();

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var search = (from p in context.People
                              join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                              from mf in pm.DefaultIfEmpty()
                              join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                              from t in pt.DefaultIfEmpty()
                              join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                              from c in pc.DefaultIfEmpty()
                              join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                              from k in pk.DefaultIfEmpty()
                              where (p.EntryStatus.Equals(StringLiteralValue.Verify) && p.ActivationStatus.Equals(StringLiteralValue.Active) && t.LanguagePrmKey == regionalLanguagePrmKey)
                                     //&& (p.FullName.Contains(_inputString) || t.TransFullName.Contains(_inputString)/* || p.PersonInformationNumber.Contains(_inputString)*/ || p.DateOfBirth.ToString().Contains(_inputString)
                                     //     || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                              orderby p.FullName
                              select new
                              {
                                  p.FullName,
                                  t.TransFullName,
                                  p.DateOfBirth,
                                  p.PersonInformationNumber,
                                  p.PersonId,
                                  c.FieldValue,
                                  k.DocumentNumber
                              }).ToList();
                var list = search.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, TransFullName1 = l.FirstOrDefault().TransFullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonId1 = l.FirstOrDefault().PersonId, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber }).ToList();
                result = list.AsEnumerable().Select(x => string.Format("{0},{1},{2},{3},{4},{5}", x.FullName1, x.TransFullName1, x.DateOfBirth1, x.PersonId1, x.PersonInformationNumber1, x.FieldValue1, x.DocumentNumber1)).ToList();
                return result;
            }

            // Default List In Default Language (i.e. English)
            var search1 = (from p in context.People
                           join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                           from mf in pm.DefaultIfEmpty()
                           join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                           from c in pc.DefaultIfEmpty()
                           join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                           from k in pk.DefaultIfEmpty()
                           where (p.EntryStatus.Equals(StringLiteralValue.Verify) && p.ActivationStatus.Equals(StringLiteralValue.Active))
                                  //&& (p.FullName.Contains(_inputString) /*|| p.PersonInformationNumber.Contains(_inputString) */|| p.DateOfBirth.ToString().Contains(_inputString)
                                  //     || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                           orderby p.FullName

                           select new
                           {
                               p.FullName,
                               p.DateOfBirth,
                               p.PersonInformationNumber,
                               p.PersonId,
                               c.FieldValue,
                               k.DocumentNumber
                           }).ToList();

            var list1 = search1.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, PersonId1 = l.FirstOrDefault().PersonId, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber.Count() }).ToList();
            result = list1.Select(x => string.Format("{0}-{1}-{2}-{3}-{4}-{5}", x.FullName1, x.DateOfBirth1, x.PersonInformationNumber1, x.PersonId1, x.FieldValue1, x.DocumentNumber1)).ToArray().ToList();
            return result;
        }

        public IEnumerable<string> GetAccountCustomerName(string _inputString)
        {
            //long personPrmKey = accountDetailRepository.GetPrmKeyById(_PersonId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            List<string> result = new List<string>();

            string[] array = { };

            if (regionalLanguagePrmKey != 1)
            {
                var search = (from p in context.People
                              join mf in context.PersonModifications.Where(m => m.EntryStatus == EntryStatus.Verified) on p.PrmKey equals mf.PersonPrmKey into pm
                              from mf in pm.DefaultIfEmpty()
                              join t in context.PersonTranslations.Where(t => t.EntryStatus == EntryStatus.Verified) on p.PrmKey equals t.PersonPrmKey into pt
                              from t in pt.DefaultIfEmpty()
                              join cd in context.CustomerAccountDetails.Where(cd => cd.EntryStatus == EntryStatus.Verified) on p.PrmKey equals cd.PersonPrmKey into cdt
                              from cd in cdt.DefaultIfEmpty()
                              join c in context.CustomerAccounts.Where(c => c.EntryStatus == EntryStatus.Verified) on cd.CustomerAccountPrmKey equals c.PrmKey into ca
                              from c in ca.DefaultIfEmpty()

                              join cd12 in context.CustomerTypes on cd.CustomerAccountPrmKey equals cd12.PrmKey into cad12
                              from cd12 in cad12.DefaultIfEmpty()

                              where (p.EntryStatus.Equals(EntryStatus.Verified))
                                      && (p.ActivationStatus.Equals(StringLiteralValue.Active))
                                      && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                      && (cd.EntryStatus.Equals(EntryStatus.Verified))
                                      && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                      && (c.EntryStatus.Equals(EntryStatus.Verified))
                                      && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                                      && (p.FirstName.ToLower().Contains(_inputString) /*|| p.PersonInformationNumber.ToLower().Contains(_inputString)*/ || p.DateOfBirth.ToString().ToLower().Contains(_inputString))
                              orderby p.FirstName
                              select new
                              {
                                  p.FullName,
                                  t.TransFullName,
                                  p.DateOfBirth,
                                  p.PersonInformationNumber,
                                  p.PersonId,
                                  cd12.NameOfCustomerType,

                              }).ToList();
                result = search.AsEnumerable().Select(x => string.Format("{0}-{1}-{2}-{3}-{4}-{5}", x.FullName, x.TransFullName, x.DateOfBirth, x.PersonInformationNumber, x.PersonId, x.NameOfCustomerType)).ToArray().ToList();
            }
            return result;

        }

        public async Task<bool> Amend(TransactionParameterViewModel _transactionParameterViewModel)
        {
            try
            {
                // Set Default Value
                _transactionParameterViewModel.EntryDateTime = DateTime.Now;
                _transactionParameterViewModel.EntryStatus = StringLiteralValue.Amend;
                _transactionParameterViewModel.ReasonForModification = "None";
                _transactionParameterViewModel.UserAction = StringLiteralValue.Amend;
                _transactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _transactionParameterViewModel.ChecksumAlgorithmPrmKey = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_transactionParameterViewModel.ChecksumAlgorithmId);

                // Mapping 
                // TransactionParameter
                TransactionParameter addressParameter = Mapper.Map<TransactionParameter>(_transactionParameterViewModel);
                TransactionParameterMakerChecker addressParameterMakerChecker = Mapper.Map<TransactionParameterMakerChecker>(_transactionParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                //addressParameter.PrmKey = _transactionParameterViewModel.TransactionParameterPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // TransactionParameter
                context.TransactionParameterMakerCheckers.Attach(addressParameterMakerChecker);
                context.Entry(addressParameterMakerChecker).State = EntityState.Added;
                addressParameter.TransactionParameterMakerCheckers.Add(addressParameterMakerChecker);

                context.TransactionParameters.Attach(addressParameter);
                context.Entry(addressParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(TransactionParameterViewModel _transactionParameterViewModel)
        {
            try
            {
                // Set Default Value
                _transactionParameterViewModel.EntryDateTime = DateTime.Now;
                _transactionParameterViewModel.UserAction = StringLiteralValue.Delete;
                _transactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                TransactionParameterMakerChecker addressParameterMakerChecker = Mapper.Map<TransactionParameterMakerChecker>(_transactionParameterViewModel);

                //TransactionParameter
                context.TransactionParameterMakerCheckers.Attach(addressParameterMakerChecker);
                context.Entry(addressParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<TransactionParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionParameterViewModel>("SELECT * FROM dbo.GetTransactionParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionParameterViewModel>> GetTransactionParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionParameterViewModel>("SELECT * FROM dbo.GetTransactionParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<TransactionParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionParameterViewModel>("SELECT * FROM dbo.GetTransactionParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<TransactionParameterViewModel> GetUnVerifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionParameterViewModel>("SELECT * FROM dbo.GetTransactionParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            //check waiting for response and rejected entries count
            int count = await context.TransactionParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(TransactionParameterViewModel _transactionParameterViewModel)
        {
            try
            {
                // Set Default Value
                _transactionParameterViewModel.EntryDateTime = DateTime.Now;
                _transactionParameterViewModel.UserAction = StringLiteralValue.Reject;
                _transactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                TransactionParameterMakerChecker addressParameterMakerChecker = Mapper.Map<TransactionParameterMakerChecker>(_transactionParameterViewModel);

                // TransactionParameter
                context.TransactionParameterMakerCheckers.Attach(addressParameterMakerChecker);
                context.Entry(addressParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(TransactionParameterViewModel _transactionParameterViewModel)
        {
            try
            {
                // Set Default Value
                _transactionParameterViewModel.EntryDateTime = DateTime.Now;
                _transactionParameterViewModel.EntryStatus = StringLiteralValue.Create;
                _transactionParameterViewModel.Remark = "None";
                _transactionParameterViewModel.UserAction = StringLiteralValue.Create;
                _transactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _transactionParameterViewModel.ChecksumAlgorithmPrmKey = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_transactionParameterViewModel.ChecksumAlgorithmId);

                // Mapping
                TransactionParameter addressParameter = Mapper.Map<TransactionParameter>(_transactionParameterViewModel);
                TransactionParameterMakerChecker addressParameterMakerChecker = Mapper.Map<TransactionParameterMakerChecker>(_transactionParameterViewModel);

                //TransactionParameter
                context.TransactionParameterMakerCheckers.Attach(addressParameterMakerChecker);
                context.Entry(addressParameterMakerChecker).State = EntityState.Added;
                addressParameter.TransactionParameterMakerCheckers.Add(addressParameterMakerChecker);

                context.TransactionParameters.Attach(addressParameter);
                context.Entry(addressParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(TransactionParameterViewModel _transactionParameterViewModel)
        {
            try
            {
                // Modify Old Record      
                TransactionParameterViewModel transactionParameterViewModelOfOldEntry = await GetActiveEntry();

                if (transactionParameterViewModelOfOldEntry.PrmKey > 0)
                {
                    // Set Default Value
                    transactionParameterViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                    transactionParameterViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                    transactionParameterViewModelOfOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];



                    // Mapping
                    TransactionParameterMakerChecker addressParameterMakerCheckerForModify = Mapper.Map<TransactionParameterMakerChecker>(transactionParameterViewModelOfOldEntry);

                    //TransactionParameter
                    context.TransactionParameterMakerCheckers.Attach(addressParameterMakerCheckerForModify);
                    context.Entry(addressParameterMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                // Set Default Value
                _transactionParameterViewModel.EntryDateTime = DateTime.Now;
                _transactionParameterViewModel.UserAction = StringLiteralValue.Verify;
                _transactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _transactionParameterViewModel.ChecksumAlgorithmPrmKey = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_transactionParameterViewModel.ChecksumAlgorithmId);

                //Mapping
                TransactionParameterMakerChecker addressParameterMakerChecker = Mapper.Map<TransactionParameterMakerChecker>(_transactionParameterViewModel);

                //TransactionParameter
                context.TransactionParameterMakerCheckers.Attach(addressParameterMakerChecker);
                context.Entry(addressParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
