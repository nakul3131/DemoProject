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
    public class EFBeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly EFDbContext context;

        public EFBeneficiaryRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            try
            {
                // Set Default Value
                _beneficiaryDetailViewModel.ActivationStatus = StringLiteralValue.Active;
                _beneficiaryDetailViewModel.EntryDateTime = DateTime.Now;
                _beneficiaryDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _beneficiaryDetailViewModel.UserAction = StringLiteralValue.Amend;
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _beneficiaryDetailViewModel.CustomerAccountTypePrmKey = GetPrmKeyById(_beneficiaryDetailViewModel.CustomerAccountTypeId);

                // Mapping 
                // BeneficiaryDetail
                BeneficiaryDetail beneficiaryDetail = Mapper.Map<BeneficiaryDetail>(_beneficiaryDetailViewModel);
                BeneficiaryDetailMakerChecker beneficiaryDetailMakerChecker = Mapper.Map<BeneficiaryDetailMakerChecker>(_beneficiaryDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                beneficiaryDetail.PrmKey = _beneficiaryDetailViewModel.BeneficiaryDetailPrmKey;
                
                // BeneficiaryDetailMakerChecker
                context.BeneficiaryDetailMakerCheckers.Attach(beneficiaryDetailMakerChecker);
                context.Entry(beneficiaryDetailMakerChecker).State = EntityState.Added;
                beneficiaryDetail.BeneficiaryDetailMakerCheckers.Add(beneficiaryDetailMakerChecker);

                context.BeneficiaryDetails.Attach(beneficiaryDetail);
                context.Entry(beneficiaryDetail).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            try
            {
                // Set Default Value
                _beneficiaryDetailViewModel.EntryDateTime = DateTime.Now;
                _beneficiaryDetailViewModel.UserAction = StringLiteralValue.Delete;
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                BeneficiaryDetailMakerChecker beneficiaryDetailMakerChecker = Mapper.Map<BeneficiaryDetailMakerChecker>(_beneficiaryDetailViewModel);

                // BeneficiaryDetailMakerChecker
                context.BeneficiaryDetailMakerCheckers.Attach(beneficiaryDetailMakerChecker);
                context.Entry(beneficiaryDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
        
        public List<SelectListItem> CustomerAccountTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1) 
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.CustomerAccountTypes                          
                            where (b.ActivationStatus.Equals(StringLiteralValue.Active))
                            orderby b.NameOfCustomerAccountType
                            orderby b.PrmKey
                            select new SelectListItem
                            {
                                Value = b.CustomerAccountTypeId.ToString(),
                                Text = (b.NameOfCustomerAccountType.Trim()),
                            }).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from b in context.CustomerAccountTypes
                        where (b.ActivationStatus.Equals(StringLiteralValue.Active))
                        orderby b.NameOfCustomerAccountType
                        orderby b.PrmKey
                        select new SelectListItem
                        {
                            Value = b.CustomerAccountTypeId.ToString(),
                            Text = (b.NameOfCustomerAccountType.Trim()),
                        }).ToList();
            }
        }

        public short GetPrmKeyById(Guid _customerAccountTypeId) 
        {
            return context.CustomerAccountTypes
                    .Where(c => c.CustomerAccountTypeId == _customerAccountTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                var aa= await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return aa;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BeneficiaryDetailViewModel> GetRejectedEntry(Guid _beneficiaryDetailId)
        {
            try
            {
                return await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntry (@BeneficiaryDetailId, @EntriesType)", new SqlParameter("@BeneficiaryDetailId", _beneficiaryDetailId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BeneficiaryDetailViewModel> GetUnVerifiedEntry(Guid _beneficiaryDetailId)
        {
            try
            {
                return await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntry (@BeneficiaryDetailId, @EntriesType)", new SqlParameter("@BeneficiaryDetailId", _beneficiaryDetailId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BeneficiaryDetailViewModel> GetVerifiedEntry(Guid _beneficiaryDetailId)
        {
            try
            {
                return await context.Database.SqlQuery<BeneficiaryDetailViewModel>("SELECT * FROM dbo.GetBeneficiaryDetailEntry (@BeneficiaryDetailId, @EntriesType)", new SqlParameter("@BeneficiaryDetailId", _beneficiaryDetailId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            try
            {
                // Set Default Value
                _beneficiaryDetailViewModel.EntryDateTime = DateTime.Now;
                _beneficiaryDetailViewModel.UserAction = StringLiteralValue.Reject;
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                BeneficiaryDetailMakerChecker beneficiaryDetailMakerChecker = Mapper.Map<BeneficiaryDetailMakerChecker>(_beneficiaryDetailViewModel);
                
                // BeneficiaryDetailMakerChecker
                context.BeneficiaryDetailMakerCheckers.Attach(beneficiaryDetailMakerChecker);
                context.Entry(beneficiaryDetailMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            try
            {
                // Set Default Value
                _beneficiaryDetailViewModel.ActivationStatus = StringLiteralValue.Active;
                _beneficiaryDetailViewModel.EntryDateTime = DateTime.Now;
                _beneficiaryDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _beneficiaryDetailViewModel.Remark = "None";
                _beneficiaryDetailViewModel.UserAction = StringLiteralValue.Create;
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _beneficiaryDetailViewModel.CustomerAccountTypePrmKey = GetPrmKeyById(_beneficiaryDetailViewModel.CustomerAccountTypeId);
                
                // Mapping
                // BeneficiaryDetail
                BeneficiaryDetail beneficiaryDetail = Mapper.Map<BeneficiaryDetail>(_beneficiaryDetailViewModel);
                BeneficiaryDetailMakerChecker beneficiaryDetailMakerChecker = Mapper.Map<BeneficiaryDetailMakerChecker>(_beneficiaryDetailViewModel);

                // BeneficiaryDetailMakerChecker
                context.BeneficiaryDetailMakerCheckers.Attach(beneficiaryDetailMakerChecker);
                context.Entry(beneficiaryDetailMakerChecker).State = EntityState.Added;
                beneficiaryDetail.BeneficiaryDetailMakerCheckers.Add(beneficiaryDetailMakerChecker);

                context.BeneficiaryDetails.Attach(beneficiaryDetail);
                context.Entry(beneficiaryDetail).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            try
            {
                // Set Default Value
                _beneficiaryDetailViewModel.EntryDateTime = DateTime.Now;
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];                
                _beneficiaryDetailViewModel.UserAction = StringLiteralValue.Verify;

                BeneficiaryDetailMakerChecker beneficiaryDetailMakerChecker = Mapper.Map<BeneficiaryDetailMakerChecker>(_beneficiaryDetailViewModel);

                // BeneficiaryDetailMakerChecker
                context.BeneficiaryDetailMakerCheckers.Attach(beneficiaryDetailMakerChecker);
                context.Entry(beneficiaryDetailMakerChecker).State = EntityState.Added;
                
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
