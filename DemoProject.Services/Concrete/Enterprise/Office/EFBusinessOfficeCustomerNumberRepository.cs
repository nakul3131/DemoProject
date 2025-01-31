using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office 
{
    public class EFBusinessOfficeCustomerNumberRepository : IBusinessOfficeCustomerNumberRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeCustomerNumberRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
    }
}
