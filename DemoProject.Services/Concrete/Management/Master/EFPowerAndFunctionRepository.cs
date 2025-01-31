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
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFPowerAndFunctionRepository : IPowerAndFunctionRepository
    {
        private readonly EFDbContext context;

        public EFPowerAndFunctionRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _powerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _powerAndFunctionViewModel.EntryStatus = StringLiteralValue.Amend;
                _powerAndFunctionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _powerAndFunctionViewModel.Remark = "None";
                _powerAndFunctionViewModel.UserAction = StringLiteralValue.Amend;
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _powerAndFunctionViewModel.ActivationStatus = StringLiteralValue.Active;

                // Get PrmKey By Id
                //_powerAndFunctionViewModel.PowerAndFunctionFor = (short)categoryRepository.GetPrmKeyById(_powerAndFunctionViewModel.PowerAndFunctionForId);
                _powerAndFunctionViewModel.ParentPrmKey = GetPrmKeyById(_powerAndFunctionViewModel.ParentId);

                PowerAndFunction powerAndFunction = Mapper.Map<PowerAndFunction>(_powerAndFunctionViewModel);
                PowerAndFunctionMakerChecker powerAndFunctionMakerChecker = Mapper.Map<PowerAndFunctionMakerChecker>(_powerAndFunctionViewModel);

                PowerAndFunctionTranslation powerAndFunctionTranslation = Mapper.Map<PowerAndFunctionTranslation>(_powerAndFunctionViewModel);
                powerAndFunctionTranslation.PrmKey = _powerAndFunctionViewModel.PowerAndFunctionTranslationPrmKey;

                PowerAndFunctionTranslationMakerChecker andFunctionTranslationMakerChecker = Mapper.Map<PowerAndFunctionTranslationMakerChecker>(_powerAndFunctionViewModel);

                context.PowerAndFunctionTranslationMakerCheckers.Attach(andFunctionTranslationMakerChecker);
                context.Entry(andFunctionTranslationMakerChecker).State = EntityState.Added;
                powerAndFunctionTranslation.PowerAndFunctionTranslationMakerCheckers.Add(andFunctionTranslationMakerChecker);

                context.PowerAndFunctionTranslations.Attach(powerAndFunctionTranslation);
                context.Entry(powerAndFunctionTranslation).State = EntityState.Modified;

                context.PowerAndFunctionMakerCheckers.Attach(powerAndFunctionMakerChecker);
                context.Entry(powerAndFunctionMakerChecker).State = EntityState.Added;
                powerAndFunction.PowerAndFunctionMakerCheckers.Add(powerAndFunctionMakerChecker);

                context.PowerAndFunctions.Attach(powerAndFunction);
                context.Entry(powerAndFunction).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _powerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _powerAndFunctionViewModel.Remark = "None";
                _powerAndFunctionViewModel.UserAction = StringLiteralValue.Delete;
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PowerAndFunctionMakerChecker powerAndFunctionMakerChecker = Mapper.Map<PowerAndFunctionMakerChecker>(_powerAndFunctionViewModel);

                PowerAndFunctionTranslationMakerChecker andFunctionTranslationMakerChecker = Mapper.Map<PowerAndFunctionTranslationMakerChecker>(_powerAndFunctionViewModel);

                context.PowerAndFunctionTranslationMakerCheckers.Attach(andFunctionTranslationMakerChecker);
                context.Entry(andFunctionTranslationMakerChecker).State = EntityState.Added;

                context.PowerAndFunctionMakerCheckers.Attach(powerAndFunctionMakerChecker);
                context.Entry(powerAndFunctionMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<PowerAndFunctionViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<PowerAndFunctionViewModel> GetRejectedEntry(Guid _PowerAndFunctionId)
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntry (@PowerAndFunctionId)", new SqlParameter("@PowerAndFunctionId", _PowerAndFunctionId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<PowerAndFunctionViewModel> GetUnverifiedEntry(Guid _PowerAndFunctionId)
        {
            try
            {
                return await context.Database.SqlQuery<PowerAndFunctionViewModel>("SELECT * FROM dbo.GetPowerAndFunctionEntry (@PowerAndFunctionId)", new SqlParameter("@PowerAndFunctionId", _PowerAndFunctionId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public int GetPrmKeyById(Guid _ParentId)
        {
            return context.PowerAndFunctions
                    .Where(c => c.PowerAndFunctionId == _ParentId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> PowerAndFunctionDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.PowerAndFunctions
                            join rt in context.PowerAndFunctionTranslations on r.PrmKey equals rt.PowerAndFunctionPrmKey
                            select new SelectListItem
                            {
                                Value = r.PowerAndFunctionId.ToString(),
                                Text = r.NameOfPowerAndFunction.Trim() + " --> " + rt.TransNameOfPowerAndFunction.Trim()
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.PowerAndFunctions

                        select new SelectListItem
                        {
                            Value = e.PowerAndFunctionId.ToString(),
                            Text = e.NameOfPowerAndFunction
                        }).ToList();
            }
        }

        public List<SelectListItem> Parents
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.PowerAndFunctions
                            join rt in context.PowerAndFunctionTranslations on r.PrmKey equals rt.PowerAndFunctionPrmKey
                            select new SelectListItem
                            {
                                Value = r.PowerAndFunctionId.ToString(),
                                Text = r.NameOfPowerAndFunction.Trim() + " --> " + rt.TransNameOfPowerAndFunction.Trim()
                            }).ToList();
                }

                return (from e in context.PowerAndFunctions

                        select new SelectListItem
                        {
                            Value = e.PowerAndFunctionId.ToString(),
                            Text = e.NameOfPowerAndFunction
                        }).ToList();
            }
        }

        public async Task<bool> Reject(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _powerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _powerAndFunctionViewModel.UserAction = StringLiteralValue.Reject;
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PowerAndFunctionMakerChecker powerAndFunctionMakerChecker = Mapper.Map<PowerAndFunctionMakerChecker>(_powerAndFunctionViewModel);

                PowerAndFunctionTranslationMakerChecker andFunctionTranslationMakerChecker = Mapper.Map<PowerAndFunctionTranslationMakerChecker>(_powerAndFunctionViewModel);

                context.PowerAndFunctionTranslationMakerCheckers.Attach(andFunctionTranslationMakerChecker);
                context.Entry(andFunctionTranslationMakerChecker).State = EntityState.Added;

                context.PowerAndFunctionMakerCheckers.Attach(powerAndFunctionMakerChecker);
                context.Entry(powerAndFunctionMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _powerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _powerAndFunctionViewModel.EntryStatus = StringLiteralValue.Create;
                _powerAndFunctionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _powerAndFunctionViewModel.Remark = "None";
                _powerAndFunctionViewModel.UserAction = StringLiteralValue.Create;
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _powerAndFunctionViewModel.ActivationStatus = StringLiteralValue.Active;

                // Get PrmKey By Id
                //_powerAndFunctionViewModel.PowerAndFunctionFor = (short)categoryRepository.GetPrmKeyById(_powerAndFunctionViewModel.PowerAndFunctionForId);
                _powerAndFunctionViewModel.ParentPrmKey = GetPrmKeyById(_powerAndFunctionViewModel.ParentId);

                PowerAndFunction powerAndFunction = Mapper.Map<PowerAndFunction>(_powerAndFunctionViewModel);
                PowerAndFunctionMakerChecker powerAndFunctionMakerChecker = Mapper.Map<PowerAndFunctionMakerChecker>(_powerAndFunctionViewModel);

                PowerAndFunctionTranslation powerAndFunctionTranslation = Mapper.Map<PowerAndFunctionTranslation>(_powerAndFunctionViewModel);
                PowerAndFunctionTranslationMakerChecker functionTranslationMakerChecker = Mapper.Map<PowerAndFunctionTranslationMakerChecker>(_powerAndFunctionViewModel);

                context.PowerAndFunctionTranslationMakerCheckers.Attach(functionTranslationMakerChecker);
                context.Entry(functionTranslationMakerChecker).State = EntityState.Added;
                powerAndFunctionTranslation.PowerAndFunctionTranslationMakerCheckers.Add(functionTranslationMakerChecker);

                context.PowerAndFunctionTranslations.Attach(powerAndFunctionTranslation);
                context.Entry(powerAndFunctionTranslation).State = EntityState.Added;
                powerAndFunction.PowerAndFunctionTranslations.Add(powerAndFunctionTranslation);

                context.PowerAndFunctionMakerCheckers.Attach(powerAndFunctionMakerChecker);
                context.Entry(powerAndFunctionMakerChecker).State = EntityState.Added;
                powerAndFunction.PowerAndFunctionMakerCheckers.Add(powerAndFunctionMakerChecker);

                context.PowerAndFunctions.Attach(powerAndFunction);
                context.Entry(powerAndFunction).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            try
            {
                // Insert New Verified Entry
                // Set Default Value
                _powerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _powerAndFunctionViewModel.UserAction = StringLiteralValue.Verify;
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PowerAndFunctionMakerChecker powerAndFunctionMakerChecker = Mapper.Map<PowerAndFunctionMakerChecker>(_powerAndFunctionViewModel);

                PowerAndFunctionTranslationMakerChecker andFunctionTranslationMakerChecker = Mapper.Map<PowerAndFunctionTranslationMakerChecker>(_powerAndFunctionViewModel);

                context.PowerAndFunctionTranslationMakerCheckers.Attach(andFunctionTranslationMakerChecker);
                context.Entry(andFunctionTranslationMakerChecker).State = EntityState.Added;

                context.PowerAndFunctionMakerCheckers.Attach(powerAndFunctionMakerChecker);
                context.Entry(powerAndFunctionMakerChecker).State = EntityState.Added;

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
