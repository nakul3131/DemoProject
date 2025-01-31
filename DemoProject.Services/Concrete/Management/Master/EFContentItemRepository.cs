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
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFContentItemRepository : IContentItemRepository
    {
        private readonly EFDbContext context;

        public EFContentItemRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.EntryStatus = StringLiteralValue.Amend;
                _contentItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _contentItemViewModel.ReasonForModification = "None";
                _contentItemViewModel.TransReasonForModification = "None";
                _contentItemViewModel.UserAction = StringLiteralValue.Amend;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //ContentItem
                ContentItem contentItem = Mapper.Map<ContentItem>(_contentItemViewModel);
                ContentItemMakerChecker contentItemMakerChecker = Mapper.Map<ContentItemMakerChecker>(_contentItemViewModel);

                //ContentItemTranslation
                ContentItemTranslation contentItemTranslation = Mapper.Map<ContentItemTranslation>(_contentItemViewModel);
                ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                //ContentItemModification
                ContentItemModification contentItemModification = Mapper.Map<ContentItemModification>(_contentItemViewModel);
                ContentItemModificationMakerChecker contentItemModificationMakerChecker = Mapper.Map<ContentItemModificationMakerChecker>(_contentItemViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                contentItem.PrmKey = _contentItemViewModel.ContentItemPrmKey;
                contentItemTranslation.PrmKey = _contentItemViewModel.ContentItemTranslationPrmKey;
                contentItemModification.PrmKey = _contentItemViewModel.ContentItemModificationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_contentItemViewModel.ContentItemModificationPrmKey == 0)
                {
                    // ContentItem
                    context.ContentItemMakerCheckers.Attach(contentItemMakerChecker);
                    context.Entry(contentItemMakerChecker).State = EntityState.Added;
                    contentItem.ContentItemMakerCheckers.Add(contentItemMakerChecker);

                    context.ContentItems.Attach(contentItem);
                    context.Entry(contentItem).State = EntityState.Modified;
                }
                else
                {
                    // ContentItem Modification 
                    context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerChecker);
                    context.Entry(contentItemModificationMakerChecker).State = EntityState.Added;
                    contentItemModification.ContentItemModificationMakerCheckers.Add(contentItemModificationMakerChecker);

                    context.ContentItemModifications.Attach(contentItemModification);
                    context.Entry(contentItemModification).State = EntityState.Modified;
                }

                //ContentItemTranslation
                context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;
                contentItemTranslation.ContentItemTranslationMakerCheckers.Add(contentItemTranslationMakerChecker);

                context.ContentItemTranslations.Attach(contentItemTranslation);
                context.Entry(contentItemTranslation).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public List<SelectListItem> ContentItemDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.ContentItems
                            join mf in context.ContentItemModifications on o.PrmKey equals mf.ContentItemPrmKey into om
                            from mf in om.DefaultIfEmpty()
                            join t in context.ContentItemTranslations on o.PrmKey equals t.ContentItemPrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.ContentItemId.ToString(),
                                Text = ((mf.NameOfContentItem.Equals(null)) ? o.NameOfContentItem.Trim() + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()) : mf.NameOfContentItem + " ---> " + (t.TransNameOfContentItem.Equals(null) ? " " : t.TransNameOfContentItem.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.ContentItems
                        join mf in context.ContentItemModifications on o.PrmKey equals mf.ContentItemPrmKey into om
                        from mf in om.DefaultIfEmpty()
                        where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = o.ContentItemId.ToString(),
                            Text = ((mf.NameOfContentItem.Equals(null)) ? o.NameOfContentItem.Trim() : mf.NameOfContentItem)
                        }).ToList();

            }
        }

        public async Task<bool> Delete(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.UserAction = StringLiteralValue.Delete;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                ContentItemMakerChecker contentItemMakerChecker = Mapper.Map<ContentItemMakerChecker>(_contentItemViewModel);
                ContentItemModificationMakerChecker contentItemModificationMakerChecker = Mapper.Map<ContentItemModificationMakerChecker>(_contentItemViewModel);
                ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                if (_contentItemViewModel.ContentItemModificationPrmKey == 0)
                {
                    // ContentItem
                    context.ContentItemMakerCheckers.Attach(contentItemMakerChecker);
                    context.Entry(contentItemMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ContentItemModification  
                    context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerChecker);
                    context.Entry(contentItemModificationMakerChecker).State = EntityState.Added;
                }

                //ContentItemTranslation
                context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<ContentItemViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ContentItemViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ContentItemViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPrmKeyById(Guid _ContentItemId)
        {
            return context.ContentItems
                    .Where(c => c.ContentItemId == _ContentItemId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<ContentItemViewModel> GetRejectedEntry(Guid _contentItemId)
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntry (@ContentItemId, @EntriesType)", new SqlParameter("@ContentItemId", _contentItemId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ContentItemViewModel> GetUnVerifiedEntry(Guid _contentItemId)
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntry (@ContentItemId, @EntriesType)", new SqlParameter("@ContentItemId", _contentItemId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueContentItemName(string _nameOfContentItem)
        {
            bool status;
            if (context.ContentItems.Where(p => p.NameOfContentItem == _nameOfContentItem).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;

        }

        public async Task<ContentItemViewModel> GetVerifiedEntry(Guid _contentItemId)
        {
            try
            {
                return await context.Database.SqlQuery<ContentItemViewModel>("SELECT * FROM dbo.GetContentItemEntry (@ContentItemId, @EntriesType)", new SqlParameter("@ContentItemId", _contentItemId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.EntryStatus = StringLiteralValue.Create;
                _contentItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _contentItemViewModel.Remark = "None";
                _contentItemViewModel.UserAction = StringLiteralValue.Create;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //ContentItemModification
                ContentItemModification contentItemModification = Mapper.Map<ContentItemModification>(_contentItemViewModel);
                ContentItemModificationMakerChecker contentItemModificationMakerChecker = Mapper.Map<ContentItemModificationMakerChecker>(_contentItemViewModel);

                //ContentItemTranslation
                ContentItemTranslation contentItemTranslation = Mapper.Map<ContentItemTranslation>(_contentItemViewModel);
                ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //ContentItemModification
                context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerChecker);
                context.Entry(contentItemModificationMakerChecker).State = EntityState.Added;
                contentItemModification.ContentItemModificationMakerCheckers.Add(contentItemModificationMakerChecker);

                context.ContentItemModifications.Attach(contentItemModification);
                context.Entry(contentItemModification).State = EntityState.Added;

                //ContentItemTranslation
                context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;
                contentItemTranslation.ContentItemTranslationMakerCheckers.Add(contentItemTranslationMakerChecker);

                context.ContentItemTranslations.Attach(contentItemTranslation);
                context.Entry(contentItemTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.UserAction = StringLiteralValue.Reject;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                ContentItemMakerChecker contentItemMakerChecker = Mapper.Map<ContentItemMakerChecker>(_contentItemViewModel);
                ContentItemModificationMakerChecker contentItemModificationMakerChecker = Mapper.Map<ContentItemModificationMakerChecker>(_contentItemViewModel);
                ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                // Check Entry Existance In Modification Table Or Main Table
                if (_contentItemViewModel.ContentItemModificationPrmKey == 0)
                {
                    // ContentItemMakerChecker
                    context.ContentItemMakerCheckers.Attach(contentItemMakerChecker);
                    context.Entry(contentItemMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ContentItemModificationMakerChecker
                    context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerChecker);
                    context.Entry(contentItemModificationMakerChecker).State = EntityState.Added;
                }

                //ContentItemTranslationMakerChecker
                context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.ActivationStatus = StringLiteralValue.Active;
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.EntryStatus = StringLiteralValue.Create;
                _contentItemViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _contentItemViewModel.Remark = "None";
                _contentItemViewModel.ReasonForModification = "None";
                _contentItemViewModel.TransReasonForModification = "None";
                _contentItemViewModel.UserAction = StringLiteralValue.Create;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //ContentItem
                ContentItem contentItem = Mapper.Map<ContentItem>(_contentItemViewModel);
                ContentItemMakerChecker contentItemMakerChecker = Mapper.Map<ContentItemMakerChecker>(_contentItemViewModel);

                //ContentItemTranslation
                ContentItemTranslation contentItemTranslation = Mapper.Map<ContentItemTranslation>(_contentItemViewModel);
                ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //ContentItem
                context.ContentItemMakerCheckers.Attach(contentItemMakerChecker);
                context.Entry(contentItemMakerChecker).State = EntityState.Added;
                contentItem.ContentItemMakerCheckers.Add(contentItemMakerChecker);

                context.ContentItems.Attach(contentItem);
                context.Entry(contentItem).State = EntityState.Added;

                //ContentItemTranslation
                context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;
                contentItemTranslation.ContentItemTranslationMakerCheckers.Add(contentItemTranslationMakerChecker);

                context.ContentItemTranslations.Attach(contentItemTranslation);
                context.Entry(contentItemTranslation).State = EntityState.Added;
                contentItem.ContentItemTranslations.Add(contentItemTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(ContentItemViewModel _contentItemViewModel)
        {
            try
            {
                // Set Default Value
                _contentItemViewModel.EntryDateTime = DateTime.Now;
                _contentItemViewModel.UserAction = StringLiteralValue.Verify;
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_contentItemViewModel.ContentItemModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    ContentItemViewModel contentItemViewModelForModify = await GetVerifiedEntry(_contentItemViewModel.ContentItemId);

                    // Set Default Value
                    contentItemViewModelForModify.UserAction = StringLiteralValue.Modify;
                    contentItemViewModelForModify.UserProfilePrmKey = _contentItemViewModel.UserProfilePrmKey;
                    // ContentItemTranslation
                    ContentItemTranslationMakerChecker contentItemTranslationMakerCheckerForModify = Mapper.Map<ContentItemTranslationMakerChecker>(contentItemViewModelForModify);

                    context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerCheckerForModify);
                    context.Entry(contentItemTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Check Entry Existance In Modification Table Or Main Table
                    if (contentItemViewModelForModify.IsModified == true)
                    {
                        ContentItemModificationMakerChecker contentItemModificationMakerCheckerForModify = Mapper.Map<ContentItemModificationMakerChecker>(contentItemViewModelForModify);

                        context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerCheckerForModify);
                        context.Entry(contentItemModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _contentItemViewModel.UserAction = StringLiteralValue.Verify;

                    ContentItemModificationMakerChecker contentItemModificationMakerChecker = Mapper.Map<ContentItemModificationMakerChecker>(_contentItemViewModel);
                    ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                    // ContentItemModificationMakerChecker
                    context.ContentItemModificationMakerCheckers.Attach(contentItemModificationMakerChecker);
                    context.Entry(contentItemModificationMakerChecker).State = EntityState.Added;

                    // ContentItemTranslationMakerChecker
                    context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                    context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _contentItemViewModel.UserAction = StringLiteralValue.Verify;

                    ContentItemMakerChecker contentItemMakerChecker = Mapper.Map<ContentItemMakerChecker>(_contentItemViewModel);
                    ContentItemTranslationMakerChecker contentItemTranslationMakerChecker = Mapper.Map<ContentItemTranslationMakerChecker>(_contentItemViewModel);

                    // ContentItemMakerChecker
                    context.ContentItemMakerCheckers.Attach(contentItemMakerChecker);
                    context.Entry(contentItemMakerChecker).State = EntityState.Added;

                    // ContentItemTranslationMakerChecker
                    context.ContentItemTranslationMakerCheckers.Attach(contentItemTranslationMakerChecker);
                    context.Entry(contentItemTranslationMakerChecker).State = EntityState.Added;
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
