using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeAccountNumberRepository : IBusinessOfficeAccountNumberRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeAccountNumberRepository(RepositoryConnection _connection) 
        {
            context = _connection.EFDbContext;
        }
    }
}
