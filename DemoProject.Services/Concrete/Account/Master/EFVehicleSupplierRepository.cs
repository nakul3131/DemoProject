using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFVehicleSupplierRepository : IVehicleSupplierRepository
    {
        private readonly EFDbContext context;

        public EFVehicleSupplierRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

    }
}
