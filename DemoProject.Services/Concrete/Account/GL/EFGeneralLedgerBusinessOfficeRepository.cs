using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerBusinessOfficeRepository : IGeneralLedgerBusinessOfficeRepository
    {
        private readonly EFDbContext context;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IGeneralLedgerDetailRepository generalLedgerDetailRepository;
        public EFGeneralLedgerBusinessOfficeRepository(RepositoryConnection _connection, IGeneralLedgerDetailRepository _generalLedgerDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository)
        {
            context = _connection.EFDbContext;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            generalLedgerDetailRepository = _generalLedgerDetailRepository;
        }

        public async Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetRejectedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerBusinessOfficeViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<GeneralLedgerIndexViewModel>> GetRejectedGeneralLedgerBusinessOfficeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerIndexViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]),  new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<GeneralLedgerIndexViewModel>> GetGeneralLedgerBusinessOfficeUnverifiedIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerIndexViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetUnverifiedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerBusinessOfficeViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetVerifiedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<GeneralLedgerBusinessOfficeViewModel>("SELECT * FROM dbo.GetGeneralLedgerBusinessOfficeEntriesByGeneralLedgerPrmKey(@UserProfilePrmKey,@GeneralLedgerPrmKey,@EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
              return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
           
            try
            {
                // GeneralLedgerBusinessOffice
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];
                if (generalLedgerBusinessOfficeViewModelList != null)
                {
                    foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                    {
                        if (viewModel.GeneralLedgerPrmKey == 0)
                        {
                            viewModel.EntryDateTime = DateTime.Now;
                            viewModel.EntryStatus = StringLiteralValue.Create;
                            viewModel.UserAction = StringLiteralValue.Create;
                            viewModel.ActivationStatus = StringLiteralValue.Active;
                            viewModel.ReasonForModification = "None";
                            viewModel.CloseDate = null;
                            viewModel.Remark = "None";
                            viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                            viewModel.GeneralLedgerPrmKey = _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey;

                            if (viewModel.Note == null)
                            {
                                viewModel.Note = "None";
                            }
                            // Set ReferenceKey As PrmKey To Every Object
                            viewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);

                            GeneralLedgerBusinessOffice generalLedgerBusinessOffice = Mapper.Map<GeneralLedgerBusinessOffice>(viewModel);
                            GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);

                            context.GeneralLedgerBusinessOffices.Attach(generalLedgerBusinessOffice);
                            context.Entry(generalLedgerBusinessOffice).State = EntityState.Added;


                            context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                            context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                            generalLedgerBusinessOffice.GeneralLedgerBusinessOfficeMakerCheckers.Add(generalLedgerBusinessOfficeMakerChecker);

                        }
                    }
                }


                await context.SaveChangesAsync();



                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            try
            {
                //GeneralLedgerBusinessOffice
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];

                foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                {
                    // Set Deafult Value

                    generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Reject, _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);

                    GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);
                    generalLedgerBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                    context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            try
            {
                // Modify Old GeneralLedgerBusinessOffice
                IEnumerable<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelListForModify = await generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeEntries(_generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey, StringLiteralValue.Verify);
                foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;

                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerCheckerForModify = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);

                    
                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerCheckerForModify);
                    context.Entry(generalLedgerBusinessOfficeMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = new List<GeneralLedgerBusinessOfficeViewModel>();
                generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];

                foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);
                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                    context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Amend(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            try
            {

                //generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(_generalLedgerBusinessOfficeViewModel, StringLiteralValue.Amend,_generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);


                // Amend Old GeneralLedgerBusinessOffice
                IEnumerable<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelListForAmend = await GetRejectedGeneralLedgerBusinessOfficeEntries(_generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);
                foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelListForAmend)
                {
                    generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(_generalLedgerBusinessOfficeViewModel, StringLiteralValue.Amend, _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);
                    _generalLedgerBusinessOfficeViewModel.PrmKey = 0;

                    GeneralLedgerBusinessOfficeMakerChecker generalLedgerCurrencyMakerCheckerForAmend = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(_generalLedgerBusinessOfficeViewModel);

                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerCurrencyMakerCheckerForAmend);
                    context.Entry(generalLedgerCurrencyMakerCheckerForAmend).State = EntityState.Added;
                }

                // GeneralLedgerBusinessOffice - Insert New Added Or Amended BusinessOffice (Because Of MultiSelect Session Object Not Required)
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];
                if (generalLedgerBusinessOfficeViewModelList != null)
                {
                    foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                    {
                        // Set Default Value
                        generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Create, _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);

                        // Get PrmKey By Id Of All Dropdowns
                        viewModel.BusinessOfficePrmKey = generalLedgerDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);

                        GeneralLedgerBusinessOffice generalLedgerBusinessOffice = Mapper.Map<GeneralLedgerBusinessOffice>(viewModel);
                        GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);

                        context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                        context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                        generalLedgerBusinessOffice.GeneralLedgerBusinessOfficeMakerCheckers.Add(generalLedgerBusinessOfficeMakerChecker);

                        context.GeneralLedgerBusinessOffices.Attach(generalLedgerBusinessOffice);
                        context.Entry(generalLedgerBusinessOffice).State = EntityState.Added;
                    }

                    
                }
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)

            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            try
            {
                // Set Default Value
                generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(_generalLedgerBusinessOfficeViewModel, StringLiteralValue.Delete, _generalLedgerBusinessOfficeViewModel.GeneralLedgerPrmKey);

                //UserRoleProfile
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];

                foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                {
                    // Set Default Value
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Delete,viewModel.GeneralLedgerPrmKey);

                    GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);

                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                    context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                }

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
