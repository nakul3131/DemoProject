using System;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonStatusRepository : IPersonStatusRepository 
    {
        private readonly EFDbContext context;

        private readonly IPersonDetailRepository personDetailRepository;

        public EFPersonStatusRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;

            personDetailRepository = _personDetailRepository;
        }

        public string GetMemberType(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            return (from s in context.PersonStatuses
                        join mm in context.MemberTypes on s.MemberTypePrmKey equals mm.PrmKey into sm
                        from mm in sm.DefaultIfEmpty()
                        join t in context.MemberTypeTranslations on mm.PrmKey equals t.MemberTypePrmKey into mt
                        from t in mt.DefaultIfEmpty()
                        where (s.PersonPrmKey == personPrmKey)
                         && (s.EntryStatus == StringLiteralValue.Verify)
                        select mm.NameOfMemberType.Trim()).FirstOrDefault();
        }

        public string GetBorrowingStatus(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            string result = "None";
            
            byte borrowingStatus = context.PersonStatuses
                                    .Where(s => s.PersonPrmKey == personPrmKey)
                                    .Select(s => s.BorrowingStatus).FirstOrDefault();
           
            if (borrowingStatus == 0)
                result = "Regular";

            if (borrowingStatus == 1)
                result = "Irregular";

            if (borrowingStatus == 2)
                result = "N.P.A.";

            return result;
        }

        public string GetGuarantorStatus(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            string result = "None";

            byte guarantorStatus = context.PersonStatuses
                                    .Where(s => s.PersonPrmKey == personPrmKey)
                                    .Select(s => s.GuarantorStatus).FirstOrDefault();

            if (guarantorStatus == 0)
                result = "Gurantor Of Regular Borrower";

            if (guarantorStatus == 1)
                result = "Gurantor Of Irregular Borrower";

            if (guarantorStatus == 2)
                result = "Gurantor Of N.P.A. Borrower";

            return result;
        }

    }
}
