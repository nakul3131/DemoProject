using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Parameter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionTypeSettingViewModel
    {
             public bool EnableCashDenomination { get; set; }
             public DateTime UserAllowedLastPastDate { get; set; }
           
            [StringLength(10)]
           public string SysNameOfTransactionType { get; set; }
           public Guid CashGeneralLedgerId { get; set; }
       
    }
}
