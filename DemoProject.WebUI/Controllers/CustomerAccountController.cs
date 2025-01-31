using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Account/CustomerAccount")]
    public class CustomerAccountController : Controller
    {
        private readonly ICustomerAccountRepository customerAccountRepository;

        public CustomerAccountController(ICustomerAccountRepository _customerAccountRepository)
        {
            customerAccountRepository = _customerAccountRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CustomerAccountId)
        {
            DepositCustomerAccountViewModel customerAccountViewModel = await customerAccountRepository.GetRejectedEntry(CustomerAccountId);

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CustomerAccountViewModel _customerAccountViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await customerAccountRepository.Amend(_customerAccountViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CustomerAccount");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await customerAccountRepository.Delete(_customerAccountViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CustomerAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_customerAccountViewModel);
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(CustomerAccountViewModel _customerAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await customerAccountRepository.Save(_customerAccountViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CustomerAccountCustomerAccount");
                    }

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_customerAccountViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CustomerAccountId)
        {
            DepositCustomerAccountViewModel customerAccountViewModel = await customerAccountRepository.GetVerifiedEntry(CustomerAccountId);//GetVerifiedEntry

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CustomerAccountViewModel _customerAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await customerAccountRepository.Modify(_customerAccountViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_customerAccountViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CustomerAccountViewModel> customerAccountViewModel = await customerAccountRepository.GetIndexOfRejectedEntries();

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CustomerAccountViewModel> customerAccountViewModel = await customerAccountRepository.GetIndexOfUnVerifiedEntries();

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CustomerAccountViewModel> customerAccountViewModel = await customerAccountRepository.GetIndexOfVerifiedEntries();

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CustomerAccountId)
        {
            DepositCustomerAccountViewModel customerAccountViewModel = await customerAccountRepository.GetUnVerifiedEntry(CustomerAccountId);

            if (customerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(CustomerAccountViewModel _customerAccountViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await customerAccountRepository.Verify(_customerAccountViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _customerAccountViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await customerAccountRepository.Reject(_customerAccountViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CustomerAccount");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_customerAccountViewModel.CustomerAccountId);
        }
    }
}