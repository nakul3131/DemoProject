using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeApplicationNumberRepository : IBusinessOfficeApplicationNumberRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeApplicationNumberRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext; 
        }     
    }
}
