using DemoProject.Services.Concrete;

namespace DemoProject.Services.Wrapper
{
    public class RepositoryConnection
    {
        public RepositoryConnection()
        {
            EFDbContext = new EFDbContext();
        }

        public EFDbContext EFDbContext { get; }
    }
}
