using System.Linq;
using DemoProject.Services.Abstract.Security.Login;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFEmergencyScreenRepository : IEmergencyScreenRepository
    {
        private readonly EFDbContext context;

        public EFEmergencyScreenRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public string HeaderText()
        {
            return context.EmergencyScreens
                        .Select(e => e.HeaderText).FirstOrDefault();
        }

        public string BodyText()
        {
            return context.EmergencyScreens
                        .Select(e => e.BodyText).FirstOrDefault();
        }

        public string FooterText()
        {
            return context.EmergencyScreens
                        .Select(e => e.FooterText).FirstOrDefault();
        }
    }
}
