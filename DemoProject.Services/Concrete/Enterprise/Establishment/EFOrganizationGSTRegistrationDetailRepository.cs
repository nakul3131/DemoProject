using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationGSTRegistrationDetailRepository : IOrganizationGSTRegistrationDetailRepository
    {
        private readonly EFDbContext context;

        public EFOrganizationGSTRegistrationDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

    }
}
