using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationContactDetailRepository : IOrganizationContactDetailRepository
    {
        private readonly EFDbContext context;

        public EFOrganizationContactDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

    }
}
