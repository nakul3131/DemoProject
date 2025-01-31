using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFFixedAssetItemRepository : IFixedAssetItemRepository
    {
        private readonly EFDbContext context;

        public EFFixedAssetItemRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.EntryStatus = StringLiteralValue.Amend;
                _fixedAssetItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _fixedAssetItemViewModel.ReasonForModification = "None";
                _fixedAssetItemViewModel.TransReasonForModification = "None";
                _fixedAssetItemViewModel.UserAction = StringLiteralValue.Amend;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // FixedAssetItem
                FixedAssetItem fixedAssetItemForAmend = Mapper.Map<FixedAssetItem>(_fixedAssetItemViewModel);
                FixedAssetItemMakerChecker fixedAssetItemMakerCheckerForAmend = Mapper.Map<FixedAssetItemMakerChecker>(_fixedAssetItemViewModel);

                // FixedAssetItemModification
                FixedAssetItemModification fixedAssetItemModificationForAmend = Mapper.Map<FixedAssetItemModification>(_fixedAssetItemViewModel);
                FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerCheckerForAmend = Mapper.Map<FixedAssetItemModificationMakerChecker>(_fixedAssetItemViewModel);
                FixedAssetItemTranslation fixedAssetItemTranslationForAmend = Mapper.Map<FixedAssetItemTranslation>(_fixedAssetItemViewModel);
                FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerCheckerForAmend = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                fixedAssetItemForAmend.PrmKey = _fixedAssetItemViewModel.FixedAssetItemPrmKey;
                fixedAssetItemModificationForAmend.PrmKey = _fixedAssetItemViewModel.FixedAssetItemModificationPrmKey;
                fixedAssetItemTranslationForAmend.PrmKey = _fixedAssetItemViewModel.FixedAssetItemTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_fixedAssetItemViewModel.FixedAssetItemModificationPrmKey == 0)
                {
                    // FixedAssetItem
                    context.FixedAssetItemMakerCheckers.Attach(fixedAssetItemMakerCheckerForAmend);
                    context.Entry(fixedAssetItemMakerCheckerForAmend).State = EntityState.Added;
                    fixedAssetItemForAmend.FixedAssetItemMakerCheckers.Add(fixedAssetItemMakerCheckerForAmend);

                    context.FixedAssetItems.Attach(fixedAssetItemForAmend);
                    context.Entry(fixedAssetItemForAmend).State = EntityState.Modified;
                }
                else
                {
                    // FixedAssetItem Modification 
                    context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerCheckerForAmend);
                    context.Entry(fixedAssetItemModificationMakerCheckerForAmend).State = EntityState.Added;
                    fixedAssetItemModificationForAmend.FixedAssetItemModificationMakerCheckers.Add(fixedAssetItemModificationMakerCheckerForAmend);

                    context.FixedAssetItemModifications.Attach(fixedAssetItemModificationForAmend);
                    context.Entry(fixedAssetItemModificationForAmend).State = EntityState.Modified;
                }

                // FixedAssetItemTranslation
                context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerCheckerForAmend);
                context.Entry(fixedAssetItemTranslationMakerCheckerForAmend).State = EntityState.Added;
                fixedAssetItemTranslationForAmend.FixedAssetItemTranslationMakerCheckers.Add(fixedAssetItemTranslationMakerCheckerForAmend);

                context.FixedAssetItemTranslations.Attach(fixedAssetItemTranslationForAmend);
                context.Entry(fixedAssetItemTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.ReasonForModification = "None";
                _fixedAssetItemViewModel.Remark = "None";
                _fixedAssetItemViewModel.UserAction = StringLiteralValue.Delete;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                FixedAssetItemMakerChecker fixedAssetItemMakerChecker = Mapper.Map<FixedAssetItemMakerChecker>(_fixedAssetItemViewModel);
                FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerChecker = Mapper.Map<FixedAssetItemModificationMakerChecker>(_fixedAssetItemViewModel);
                FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                if (_fixedAssetItemViewModel.FixedAssetItemModificationPrmKey == 0)
                {
                    // FixedAssetItem
                    context.FixedAssetItemMakerCheckers.Attach(fixedAssetItemMakerChecker);
                    context.Entry(fixedAssetItemMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // FixedAssetItemModification  
                    context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerChecker);
                    context.Entry(fixedAssetItemModificationMakerChecker).State = EntityState.Added;
                }

                // FixedAssetItemTranslation
                context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public List<SelectListItem> FixedAssetItemDropdownList
        {
            get
            {
                //Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                //If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.FixedAssetItems
                            join mf in context.FixedAssetItemModifications on d.PrmKey equals mf.FixedAssetItemPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.FixedAssetItemTranslations on d.PrmKey equals t.FixedAssetItemPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    || (d.EntryStatus == StringLiteralValue.Verify)
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (d.IsModified.Equals(false))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby d.NameOfItem
                            select new SelectListItem
                            {
                                Value = d.FixedAssetItemId.ToString(),
                                Text = ((mf.NameOfItem.Equals(null)) ? d.NameOfItem.Trim() + " ---> " + (t.TransNameOfItem.Equals(null) ? " " : t.TransNameOfItem.Trim()) : mf.NameOfItem + " ---> " + (t.TransNameOfItem.Equals(null) ? " " : t.TransNameOfItem.Trim()))
                            }).ToList();
                }

                //Default List In Defaul Language(i.e.English)
                return (from d in context.FixedAssetItems
                        join mf in context.FixedAssetItemModifications on d.PrmKey equals mf.FixedAssetItemPrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = d.FixedAssetItemId.ToString(),
                            Text = ((mf.NameOfItem.Equals(null)) ? d.NameOfItem.Trim() : mf.NameOfItem)
                        }).ToList();
            }
        }

        public async Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPrmKeyById(Guid _fixedAssetItemId)
        {
            return context.FixedAssetItems
                    .Where(c => c.FixedAssetItemId == _fixedAssetItemId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<FixedAssetItemViewModel> GetRejectedEntry(Guid _fixedAssetItemId)
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntry (@FixedAssetItemId, @EntriesType)", new SqlParameter("@FixedAssetItemId", _fixedAssetItemId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<FixedAssetItemViewModel> GetUnVerifiedEntry(Guid _fixedAssetItemId)
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntry (@FixedAssetItemId, @EntriesType)", new SqlParameter("@FixedAssetItemId", _fixedAssetItemId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<FixedAssetItemViewModel> GetVerifiedEntry(Guid _fixedAssetItemId)
        {
            try
            {
                return await context.Database.SqlQuery<FixedAssetItemViewModel>("SELECT * FROM dbo.GetFixedAssetItemEntry (@FixedAssetItemId, @EntriesType)", new SqlParameter("@FixedAssetItemId", _fixedAssetItemId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.FixedAssetItemModificationPrmKey = 0;
                _fixedAssetItemViewModel.FixedAssetItemTranslationPrmKey = 0;
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.EntryStatus = StringLiteralValue.Create;
                _fixedAssetItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _fixedAssetItemViewModel.Remark = "None";
                _fixedAssetItemViewModel.UserAction = StringLiteralValue.Create;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // FixedAssetItemModification
                FixedAssetItemModification fixedAssetItemModification = Mapper.Map<FixedAssetItemModification>(_fixedAssetItemViewModel);
                FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerChecker = Mapper.Map<FixedAssetItemModificationMakerChecker>(_fixedAssetItemViewModel);

                // FixedAssetItemnTranslation
                FixedAssetItemTranslation fixedAssetItemTranslation = Mapper.Map<FixedAssetItemTranslation>(_fixedAssetItemViewModel);
                FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                // FixedAssetItemModification
                context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerChecker);
                context.Entry(fixedAssetItemModificationMakerChecker).State = EntityState.Added;
                fixedAssetItemModification.FixedAssetItemModificationMakerCheckers.Add(fixedAssetItemModificationMakerChecker);

                context.FixedAssetItemModifications.Attach(fixedAssetItemModification);
                context.Entry(fixedAssetItemModification).State = EntityState.Added;

                // FixedAssetItemTranslation
                context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;
                fixedAssetItemTranslation.FixedAssetItemTranslationMakerCheckers.Add(fixedAssetItemTranslationMakerChecker);

                context.FixedAssetItemTranslations.Attach(fixedAssetItemTranslation);
                context.Entry(fixedAssetItemTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.UserAction = StringLiteralValue.Reject;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                FixedAssetItemMakerChecker fixedAssetItemMakerChecker = Mapper.Map<FixedAssetItemMakerChecker>(_fixedAssetItemViewModel);
                FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerChecker = Mapper.Map<FixedAssetItemModificationMakerChecker>(_fixedAssetItemViewModel);
                FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_fixedAssetItemViewModel.FixedAssetItemModificationPrmKey == 0)
                {
                    // FixedAssetItemMakerChecker
                    context.FixedAssetItemMakerCheckers.Attach(fixedAssetItemMakerChecker);
                    context.Entry(fixedAssetItemMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // FixedAssetItemModificationMakerChecker
                    context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerChecker);
                    context.Entry(fixedAssetItemModificationMakerChecker).State = EntityState.Added;
                }

                // FixedAssetItemTranslationMakerChecker
                context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.EntryStatus = StringLiteralValue.Create;
                _fixedAssetItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _fixedAssetItemViewModel.ReasonForModification = "None";
                _fixedAssetItemViewModel.Remark = "None";
                _fixedAssetItemViewModel.TransReasonForModification = "None";
                _fixedAssetItemViewModel.UserAction = StringLiteralValue.Create;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // FixedAssetItem
                FixedAssetItem fixedAssetItem = Mapper.Map<FixedAssetItem>(_fixedAssetItemViewModel);
                FixedAssetItemMakerChecker fixedAssetItemMakerChecker = Mapper.Map<FixedAssetItemMakerChecker>(_fixedAssetItemViewModel);

                // FixedAssetItemTranslation
                FixedAssetItemTranslation fixedAssetItemTranslation = Mapper.Map<FixedAssetItemTranslation>(_fixedAssetItemViewModel);
                FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                // FixedAssetItemMakerChecker
                context.FixedAssetItemMakerCheckers.Attach(fixedAssetItemMakerChecker);
                context.Entry(fixedAssetItemMakerChecker).State = EntityState.Added;
                fixedAssetItem.FixedAssetItemMakerCheckers.Add(fixedAssetItemMakerChecker);

                context.FixedAssetItems.Attach(fixedAssetItem);
                context.Entry(fixedAssetItem).State = EntityState.Added;

                // FixedAssetItemTranslationMakerChecker
                context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;
                fixedAssetItemTranslation.FixedAssetItemTranslationMakerCheckers.Add(fixedAssetItemTranslationMakerChecker);

                context.FixedAssetItemTranslations.Attach(fixedAssetItemTranslation);
                context.Entry(fixedAssetItemTranslation).State = EntityState.Added;
                fixedAssetItem.FixedAssetItemTranslations.Add(fixedAssetItemTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            try
            {
                // Set Default Value
                _fixedAssetItemViewModel.EntryDateTime = DateTime.Now;
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_fixedAssetItemViewModel.FixedAssetItemModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    FixedAssetItemViewModel fixedAssetItemViewModelForModify = await GetVerifiedEntry(_fixedAssetItemViewModel.FixedAssetItemId);

                    // Set Default Value
                    fixedAssetItemViewModelForModify.UserAction = StringLiteralValue.Modify;
                    fixedAssetItemViewModelForModify.UserProfilePrmKey = _fixedAssetItemViewModel.UserProfilePrmKey;

                    // FixedAssetItemTranslation
                    FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerCheckerForModify = Mapper.Map<FixedAssetItemTranslationMakerChecker>(fixedAssetItemViewModelForModify);

                    context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerCheckerForModify);
                    context.Entry(fixedAssetItemTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (fixedAssetItemViewModelForModify.IsModified == true)
                    {
                        FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerCheckerForModify = Mapper.Map<FixedAssetItemModificationMakerChecker>(fixedAssetItemViewModelForModify);

                        context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerCheckerForModify);
                        context.Entry(fixedAssetItemModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _fixedAssetItemViewModel.UserAction = StringLiteralValue.Verify;

                    FixedAssetItemModificationMakerChecker fixedAssetItemModificationMakerChecker = Mapper.Map<FixedAssetItemModificationMakerChecker>(_fixedAssetItemViewModel);
                    FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                    // FixedAssetItemModificationMakerChecker
                    context.FixedAssetItemModificationMakerCheckers.Attach(fixedAssetItemModificationMakerChecker);
                    context.Entry(fixedAssetItemModificationMakerChecker).State = EntityState.Added;

                    // FixedAssetItemTranslationMakerChecker
                    context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                    context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _fixedAssetItemViewModel.UserAction = StringLiteralValue.Verify;

                    FixedAssetItemMakerChecker fixedAssetItemMakerChecker = Mapper.Map<FixedAssetItemMakerChecker>(_fixedAssetItemViewModel);
                    FixedAssetItemTranslationMakerChecker fixedAssetItemTranslationMakerChecker = Mapper.Map<FixedAssetItemTranslationMakerChecker>(_fixedAssetItemViewModel);

                    // FixedAssetItemMakerChecker
                    context.FixedAssetItemMakerCheckers.Attach(fixedAssetItemMakerChecker);
                    context.Entry(fixedAssetItemMakerChecker).State = EntityState.Added;

                    // FixedAssetItemTranslationMakerChecker
                    context.FixedAssetItemTranslationMakerCheckers.Attach(fixedAssetItemTranslationMakerChecker);
                    context.Entry(fixedAssetItemTranslationMakerChecker).State = EntityState.Added;
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
