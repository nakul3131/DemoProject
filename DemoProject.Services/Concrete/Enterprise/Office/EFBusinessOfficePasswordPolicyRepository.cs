using AutoMapper;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficePasswordPolicyRepository : IBusinessOfficePasswordPolicyRepository
    {
        private readonly EFDbContext context;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public EFBusinessOfficePasswordPolicyRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
            securityDetailRepository = _securityDetailRepository;
        }

        public async Task<bool> Amend(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                IEnumerable<BusinessOfficePasswordPolicyViewModel> OfficePasswordPolicyViewModelList = await officeDetailRepository.GetPasswordPolicyEntries(_businessOfficePasswordPolicyViewModel.BusinessOfficePrmKey,StringLiteralValue.Reject);
                foreach (BusinessOfficePasswordPolicyViewModel viewModel in OfficePasswordPolicyViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                }

                // Get BusinessOfficePasswordPolicy From Session Object
                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];
                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.BusinessOfficePrmKey = _businessOfficePasswordPolicyViewModel.BusinessOfficePrmKey;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = _businessOfficePasswordPolicyViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(viewModel.PasswordPolicyId);

                    BusinessOfficePasswordPolicy businessOfficePasswordPolicy = Mapper.Map<BusinessOfficePasswordPolicy>(viewModel);
                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                    businessOfficePasswordPolicy.BusinessOfficePasswordPolicyMakerCheckers.Add(businessOfficePasswordPolicyMakerChecker);

                    context.BusinessOfficePasswordPolicies.Attach(businessOfficePasswordPolicy);
                    context.Entry(businessOfficePasswordPolicy).State = EntityState.Added;
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

        public async Task<bool> Delete(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];

                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                {
                    // Set Default Value 
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = _businessOfficePasswordPolicyViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Closed(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = new List<BusinessOfficePasswordPolicyViewModel>();

                businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];

                if (_businessOfficePasswordPolicyViewModel.CloseDate != null)
                {
                    try
                    {
                        // Get BusinessOfficePasswordPolicy From Session Object
                        foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                        {
                            // Set Default Value
                            viewModel.BusinessOfficePrmKey = _businessOfficePasswordPolicyViewModel.BusinessOfficePrmKey;
                            viewModel.ActivationStatus = "CLS";
                            viewModel.EntryDateTime = DateTime.Now;
                            viewModel.EntryStatus = StringLiteralValue.Create;
                            viewModel.Note = "None";
                            viewModel.ReasonForModification = "None";
                            viewModel.Remark = "None";
                            viewModel.UserAction = StringLiteralValue.Create;
                            viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                            // Get PrmKey By Id Of All Dropdowns
                            viewModel.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(viewModel.PasswordPolicyId);

                            BusinessOfficePasswordPolicy businessOfficePasswordPolicy = Mapper.Map<BusinessOfficePasswordPolicy>(viewModel);
                            BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                            context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                            context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                            businessOfficePasswordPolicy.BusinessOfficePasswordPolicyMakerCheckers.Add(businessOfficePasswordPolicyMakerChecker);

                            context.BusinessOfficePasswordPolicies.Attach(businessOfficePasswordPolicy);
                            context.Entry(businessOfficePasswordPolicy).State = EntityState.Added;
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

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];

                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                {
                    // Set Dafault Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _businessOfficePasswordPolicyViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
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

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                // Get BusinessOfficePasswordPolicy From Session Object
                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];

                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                {
                    // Set Default Value
                    viewModel.BusinessOfficePrmKey = _businessOfficePasswordPolicyViewModel.BusinessOfficePrmKey;
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(viewModel.PasswordPolicyId);

                    BusinessOfficePasswordPolicy businessOfficePasswordPolicy = Mapper.Map<BusinessOfficePasswordPolicy>(viewModel);
                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                    businessOfficePasswordPolicy.BusinessOfficePasswordPolicyMakerCheckers.Add(businessOfficePasswordPolicyMakerChecker);

                    context.BusinessOfficePasswordPolicies.Attach(businessOfficePasswordPolicy);
                    context.Entry(businessOfficePasswordPolicy).State = EntityState.Added;
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

        public async Task<bool> Verify(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            try
            {
                IEnumerable<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelListForModify = await officeDetailRepository.GetPasswordPolicyEntries(_businessOfficePasswordPolicyViewModel.BusinessOfficePrmKey,StringLiteralValue.Verify);
                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelListForModify)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                }

                List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = new List<BusinessOfficePasswordPolicyViewModel>();
                businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];
                foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(viewModel);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
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
