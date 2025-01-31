using System;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonStatusRepository
    {
        // Get MemberType
        string GetMemberType(Guid _personId);

        // Get BorrowingStatus
       string GetBorrowingStatus(Guid _personId);

        // Get GuarantorStatus
        string GetGuarantorStatus(Guid _personId);
    }
}
