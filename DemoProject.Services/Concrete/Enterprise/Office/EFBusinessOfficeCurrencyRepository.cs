using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{

    public class EFBusinessOfficeCurrencyRepository : IBusinessOfficeCurrencyRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeCurrencyRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
    }
}
