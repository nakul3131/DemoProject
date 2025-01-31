using DemoProject.Services.ViewModel.Account.Layout;
using System.Collections.Generic;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanAccountOpeningConfigViewModel
    {
        public SchemeAccountParameterViewModel SchemeAccountParameterViewModel { get; set; }

        public SchemeApplicationParameterViewModel SchemeApplicationParameterViewModel { get; set; }

        public SchemeLoanAccountParameterViewModel SchemeLoanAccountParameterViewModel { get; set; }

        public SchemeTenureViewModel SchemeTenureViewModel { get; set; }

        public SchemeCustomerAccountNumberViewModel SchemeCustomerAccountNumberViewModel { get; set; }

        public SchemeLoanAgreementNumberViewModel SchemeLoanAgreementNumberViewModel { get; set; }

        public SchemePassbookViewModel SchemePassbookViewModel { get; set; }

        public SchemeGoldLoanParameterViewModel SchemeGoldLoanParameterViewModel { get; set; }

        public IEnumerable<SchemeDocumentViewModel> SchemeDocumentViewModel { get; set; }

        public SchemeLoanInterestParameterViewModel SchemeLoanInterestParameterViewModel { get; set; }

        public IEnumerable<SchemeVehicleTypeLoanParameterViewModel> SchemeVehicleTypeLoanParameterViewModel { get; set; }

        public IEnumerable<SchemePreownedVehicleLoanParameterViewModel> SchemePreownedVehicleLoanParameterViewModels { get; set; }

        public SchemePreownedVehicleLoanParameterViewModel SchemePreownedVehicleLoanParameterViewModel { get; set; }

        public SchemeCashCreditLoanParameterViewModel SchemeCashCreditLoanParameterViewModel { get; set; }
        public SchemeEducationLoanParameterViewModel SchemeEducationLoanParameterViewModel { get; set; }
        public SchemeBusinessLoanViewModel SchemeBusinessLoanViewModel { get; set; }
    }
}
