namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountLimitStatusRepository
    {
        // Return Maximum Number Of Transaction Limit
        bool IsReachedMaximumNumberOfTransactionLimit(long _customerAccountPrmKey);
    }
}
