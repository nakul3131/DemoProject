using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeMenuRepository : IBusinessOfficeMenuRepository
    {
        private readonly EFDbContext context;
        //private readonly IBusinessOfficeMenuRepository businessOfficeMenuRepository; 

        public EFBusinessOfficeMenuRepository(RepositoryConnection _connection) 
        {
            context = _connection.EFDbContext;
        }

        public short GetPrmKeyById(Guid _businessOfficeId)
        {
            return context.BusinessOffices
                    .Where(c => c.BusinessOfficeId == _businessOfficeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> MenuDropdownList 
        {
            get

            {
                var s = (from a in context.Menus
                         where (a.ParentMenuPrmKey == 0)
                         select new SelectListItem
                         {
                             Value = a.MenuId.ToString(),
                             Text = a.NameOfMenu
                         }).ToList();
                return s;
            }
        }

        public async Task<IEnumerable<BusinessOfficeMenuViewModel>> GetRejectedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeMenuViewModel>("SELECT * FROM dbo.GetBusinessOfficeMenuEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeMenuViewModel>> GetUnverifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<BusinessOfficeMenuViewModel>("SELECT * FROM dbo.GetBusinessOfficeMenuEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeMenuViewModel>> GetVerifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeMenuViewModel>("SELECT * FROM dbo.GetBusinessOfficeMenuEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }       
    }
}
