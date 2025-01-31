using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonDeathDocumentRepository : IPersonDeathDocumentRepository
    {

        private readonly EFDbContext context;

        public EFPersonDeathDocumentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }


        public async Task<IEnumerable<PersonDeathDocumentViewModel>> GetRejectedEntries(long _personPrmKey)
        {
            try
            {
                IEnumerable<PersonDeathDocumentViewModel> personDeathDocumentViewModels;

                personDeathDocumentViewModels = await context.Database.SqlQuery<PersonDeathDocumentViewModel>("SELECT * FROM dbo.GetPersonDeathDocumentEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

                return personDeathDocumentViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathDocumentViewModel>> GetUnverifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathDocumentViewModel>("SELECT * FROM dbo.GetPersonDeathDocumentEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathDocumentViewModel>> GetVerifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathDocumentViewModel>("SELECT * FROM dbo.GetPersonDeathDocumentEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

    }
}
