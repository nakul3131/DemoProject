using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface IAccountParameterDetailRepository
    {
        ByLawsLoanScheduleParameterViewModel GetByLawsLoanScheduleParameterEntry(short _schemePrmKey, string _entryType);
    }

}

