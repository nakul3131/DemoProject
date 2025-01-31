using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFEvaluationSectorContentItemRepository : IEvaluationSectorContentItemRepository
    {
        private readonly EFDbContext context;
        private readonly IContentItemRepository contentItemRepository;
        private readonly IEvaluationSectionRepository evaluationSectionRepository;

        public EFEvaluationSectorContentItemRepository(RepositoryConnection _connection, IContentItemRepository _contentItemRepository, IEvaluationSectionRepository _evaluationSectionRepository)
        {
            context = _connection.EFDbContext;

            contentItemRepository = _contentItemRepository;
            evaluationSectionRepository = _evaluationSectionRepository;
        }

        public async Task<bool> Amend(EvaluationSectorContentItemViewModel _evaluationSectionVariantViewModel)
        {
            try
            {
                // Amend Old EvaluationSectorContentItem
                IEnumerable<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModelsList = await GetRejectedEntries(_evaluationSectionVariantViewModel.EvaluationSectionPrmKey);
                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModelsList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMaker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMaker);
                    context.Entry(evaluationSectionVariantMaker).State = EntityState.Added;
                }

                // Get Trading Entity Details From Session Object
                List<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModelList = new List<EvaluationSectorContentItemViewModel>();

                evaluationSectionVariantViewModelList = (List<EvaluationSectorContentItemViewModel>)HttpContext.Current.Session["EvaluationSectorContentItem"];

                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModelList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Inactive;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = _evaluationSectionVariantViewModel.Note;
                    viewModel.Remark = _evaluationSectionVariantViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.EvaluationSectionPrmKey = _evaluationSectionVariantViewModel.EvaluationSectionPrmKey;
                    viewModel.ContentItemPrmKey = contentItemRepository.GetPrmKeyById(viewModel.ContentItemId);

                    //ViewModel
                    EvaluationSectorContentItem evaluationSectionVariant = Mapper.Map<EvaluationSectorContentItem>(viewModel);
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMakerChecker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMakerChecker);
                    context.Entry(evaluationSectionVariantMakerChecker).State = EntityState.Added;
                    evaluationSectionVariant.EvaluationSectorContentItemMakerCheckers.Add(evaluationSectionVariantMakerChecker);

                    context.EvaluationSectorContentItems.Attach(evaluationSectionVariant);
                    context.Entry(evaluationSectionVariant).State = EntityState.Added;

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

        public async Task<bool> Delete(EvaluationSectorContentItemViewModel _evaluationSectionVariantViewModel)
        {
            try
            {
                List<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModels = new List<EvaluationSectorContentItemViewModel>();

                evaluationSectionVariantViewModels = (List<EvaluationSectorContentItemViewModel>)HttpContext.Current.Session["EvaluationSectorContentItem"];

                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = _evaluationSectionVariantViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMakerChecker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMakerChecker);
                    context.Entry(evaluationSectionVariantMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntries  ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public List<SelectListItem> EvaluationDropdownListForCreate(Guid evaluationSectionId)
        {
            List<SelectListItem> VariantNames = new List<SelectListItem>();
            short EvaluationSectionPrmKey = evaluationSectionRepository.GetPrmKeyById(evaluationSectionId);

            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                // Get ContentItemId From EvaluationSectorContentItems Having EntryStatus = Verified and Rejected To Remove From List
                IList<string> dropdownExceptList = (from vm in context.EvaluationSectorContentItems
                                                    join v in context.ContentItems on vm.ContentItemPrmKey equals v.PrmKey
                                                    where (vm.EvaluationSectionPrmKey.Equals(EvaluationSectionPrmKey) && (vm.EntryStatus.Equals(StringLiteralValue.Verify) || vm.EntryStatus.Equals(StringLiteralValue.Reject)))
                                                    select v.ContentItemId.ToString()).ToList();

                // Get All Valid Selectlist From ContentItems            
                IList<SelectListItem> dropdownListAll = (from v in context.ContentItems
                                                         join m in context.ContentItemModifications on v.PrmKey equals m.ContentItemPrmKey into vm
                                                         from m in vm.DefaultIfEmpty()
                                                         join t in context.ContentItemTranslations on v.PrmKey equals t.ContentItemPrmKey into vt
                                                         from t in vt.DefaultIfEmpty()
                                                         where (v.EntryStatus.Equals(StringLiteralValue.Verify))
                                                                 && (m.EntryStatus.Equals(StringLiteralValue.Verify) || m.EntryStatus.Equals(null))
                                                                 && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                                                 && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                                                 || (v.EntryStatus == StringLiteralValue.Verify)
                                                                 && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                                                 && (v.IsModified.Equals(false))
                                                         select new SelectListItem
                                                         {
                                                             Value = v.ContentItemId.ToString(),
                                                             Text = ((m.NameOfContentItem.Equals(null)) ? v.NameOfContentItem.Trim() + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()) : m.NameOfContentItem + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()))
                                                         }).ToList();

                // Remove Except List From AllValidList
                return (from a in dropdownListAll
                        where (!(dropdownExceptList).Contains(a.Value))
                        select new SelectListItem
                        {
                            Value = a.Value,
                            Text = a.Text
                        }).ToList();
            }

            // Default List In Defaul Language (i.e. English)
            return (from v in context.ContentItems
                    join mf in context.ContentItemModifications on v.PrmKey equals mf.ContentItemPrmKey into bm
                    from mf in bm.DefaultIfEmpty()
                    where (!(from m in context.EvaluationSectorContentItems
                             where m.EvaluationSectionPrmKey.Equals(EvaluationSectionPrmKey)
                             select m.ContentItemPrmKey).Contains(v.PrmKey)
                                         && v.EntryStatus.Equals(StringLiteralValue.Verify)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                    select new SelectListItem
                    {
                        Value = v.ContentItemId.ToString(),
                        Text = ((mf.NameOfContentItem.Equals(null)) ? v.NameOfContentItem.Trim() : mf.NameOfContentItem)
                    }).ToList();
        }

        public List<SelectListItem> EvaluationDropdownListForEdit(Guid _evaluationSectionId, Guid _contentItemId)
        {
            List<SelectListItem> VariantNames = new List<SelectListItem>();

            short EvaluationSectionPrmKey = evaluationSectionRepository.GetPrmKeyById(_evaluationSectionId);
            short ContentItemPrmKey = contentItemRepository.GetPrmKeyById(_contentItemId);

            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];



            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                // Get ContentItemId From EvaluationSectorContentItems Having EntryStatus = Verified and Rejected To Remove From List, And Not Equal To Selected Vehicle Variant (i.e. ContentItemPrmKey) For Appearing In Dropdownlist
                IList<string> dropdownExceptList = (from vm in context.EvaluationSectorContentItems
                                                    join v in context.ContentItems on vm.ContentItemPrmKey equals v.PrmKey
                                                    where (vm.EvaluationSectionPrmKey.Equals(EvaluationSectionPrmKey) && (vm.EntryStatus.Equals(StringLiteralValue.Verify) || vm.EntryStatus.Equals(StringLiteralValue.Reject)) && (v.PrmKey != ContentItemPrmKey))
                                                    select v.ContentItemId.ToString()).ToList();

                // Get All Valid Selectlist From ContentItems            
                IList<SelectListItem> dropdownListAll = (from v in context.ContentItems
                                                         join m in context.ContentItemModifications on v.PrmKey equals m.ContentItemPrmKey into vm
                                                         from m in vm.DefaultIfEmpty()
                                                         join t in context.ContentItemTranslations on v.PrmKey equals t.ContentItemPrmKey into vt
                                                         from t in vt.DefaultIfEmpty()
                                                         where (v.EntryStatus.Equals(StringLiteralValue.Verify))
                                                                 && (m.EntryStatus.Equals(StringLiteralValue.Verify) || m.EntryStatus.Equals(null))
                                                                 && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                                                 && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                                                 || (v.EntryStatus == StringLiteralValue.Verify)
                                                                 && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                                                 && (v.IsModified.Equals(false))
                                                         select new SelectListItem
                                                         {
                                                             Value = v.ContentItemId.ToString(),
                                                             Text = ((m.NameOfContentItem.Equals(null)) ? v.NameOfContentItem.Trim() + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()) : m.NameOfContentItem + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()))
                                                         }).ToList();

                // Remove Except List From AllValidList
                return (from a in dropdownListAll
                        where (!(dropdownExceptList).Contains(a.Value))
                        select new SelectListItem
                        {
                            Value = a.Value,
                            Text = a.Text
                        }).ToList();
            }

            // Default List In Defaul Language (i.e. English)
            return (from v in context.ContentItems
                    join mf in context.ContentItemModifications on v.PrmKey equals mf.ContentItemPrmKey into bm
                    from mf in bm.DefaultIfEmpty()
                    where (!(from m in context.EvaluationSectorContentItems
                             where m.EvaluationSectionPrmKey.Equals(EvaluationSectionPrmKey)
                             select m.ContentItemPrmKey).Contains(v.PrmKey)
                                         && v.EntryStatus.Equals(StringLiteralValue.Verify)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                    select new SelectListItem
                    {
                        Value = v.ContentItemId.ToString(),
                        Text = ((mf.NameOfContentItem.Equals(null)) ? v.NameOfContentItem.Trim() : mf.NameOfContentItem)
                    }).ToList();
        }

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetRejectedEntries(short _evaluationSectionPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _evaluationSectionPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetUnverifiedEntries(short _evaluationSectionPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _evaluationSectionPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetVerifiedEntries(short _evaluationSectionPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _evaluationSectionPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EvaluationSectorContentItemViewModel> GetViewModelForCreate(short _vehicleMakePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @EvaluationSectionPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EvaluationSectionPrmKey", _vehicleMakePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EvaluationSectorContentItemViewModel> GetViewModelForReject(short _evaluationSectionPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _evaluationSectionPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EvaluationSectorContentItemViewModel> GetViewModelForUnverified(short _evaluationSectionPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<EvaluationSectorContentItemViewModel>("SELECT * FROM dbo.GetEvaluationSectorContentItemEntriesByEvaluationSectionPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _evaluationSectionPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(EvaluationSectorContentItemViewModel _evaluationSectionVariantViewModel)
        {
            try
            {
                List<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModels = new List<EvaluationSectorContentItemViewModel>();

                evaluationSectionVariantViewModels = (List<EvaluationSectorContentItemViewModel>)HttpContext.Current.Session["EvaluationSectorContentItem"];

                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _evaluationSectionVariantViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMakerChecker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMakerChecker);
                    context.Entry(evaluationSectionVariantMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(EvaluationSectorContentItemViewModel _evaluationSectionVariantViewModel)
        {
            try
            {

                List<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModels = new List<EvaluationSectorContentItemViewModel>();
                evaluationSectionVariantViewModels = (List<EvaluationSectorContentItemViewModel>)HttpContext.Current.Session["EvaluationSectorContentItem"];

                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModels)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Inactive;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.Note = _evaluationSectionVariantViewModel.Note;

                    //Get PrmKey By Id
                    viewModel.EvaluationSectionPrmKey = _evaluationSectionVariantViewModel.EvaluationSectionPrmKey;
                    viewModel.ContentItemPrmKey = contentItemRepository.GetPrmKeyById(viewModel.ContentItemId);

                    //Mapping
                    EvaluationSectorContentItem evaluationSectionVariant = Mapper.Map<EvaluationSectorContentItem>(viewModel);
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMakerChecker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMakerChecker);
                    context.Entry(evaluationSectionVariantMakerChecker).State = EntityState.Added;
                    evaluationSectionVariant.EvaluationSectorContentItemMakerCheckers.Add(evaluationSectionVariantMakerChecker);

                    context.EvaluationSectorContentItems.Attach(evaluationSectionVariant);
                    context.Entry(evaluationSectionVariant).State = EntityState.Added;
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

        public async Task<bool> Verify(EvaluationSectorContentItemViewModel _evaluationSectionVariantViewModel)
        {
            try
            {
                List<EvaluationSectorContentItemViewModel> evaluationSectionVariantViewModels = new List<EvaluationSectorContentItemViewModel>();

                evaluationSectionVariantViewModels = (List<EvaluationSectorContentItemViewModel>)HttpContext.Current.Session["EvaluationSectorContentItem"];

                foreach (EvaluationSectorContentItemViewModel viewModel in evaluationSectionVariantViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.PrmKey = 0;
                    viewModel.Remark = _evaluationSectionVariantViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping 
                    EvaluationSectorContentItemMakerChecker evaluationSectionVariantMakerChecker = Mapper.Map<EvaluationSectorContentItemMakerChecker>(viewModel);

                    //EvaluationSectorContentItem
                    context.EvaluationSectorContentItemMakerCheckers.Attach(evaluationSectionVariantMakerChecker);
                    context.Entry(evaluationSectionVariantMakerChecker).State = EntityState.Added;
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
