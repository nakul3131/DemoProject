using AutoMapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Wrapper;
using System.Linq;
using System.Data.SqlClient;
using DemoProject.Services.Constants;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation.Master;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCenterISOCodeRepository : ICenterISOCodeRepository
    {
        private readonly EFDbContext context;

        public EFCenterISOCodeRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> ModifyCenterIsoCode(CenterIsoCodeViewModel _centerIsoCodeViewModel)
        {
            try
            {
                _centerIsoCodeViewModel.PrmKey = 0;
                _centerIsoCodeViewModel.CenterISOCodePrmKey = 0;

                _centerIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                Center center = new Center();

                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_centerIsoCodeViewModel);

                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_centerIsoCodeViewModel);


                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                centerISOCode.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerChecker);

                context.CenterISOCodes.Attach(centerISOCode);
                context.Entry(centerISOCode).State = EntityState.Added;
                center.CenterISOCodes.Add(centerISOCode);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public short GetPrmKeyById(Guid _centerId)
        {
            return context.Centers
                    .Where(c => c.CenterId == _centerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<CenterIsoCodeViewModel> GetRejectedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterIsoCodeViewModel>("SELECT * FROM dbo.GetCenterISOCodeEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterIsoCodeViewModel> GetUnverifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterIsoCodeViewModel>("SELECT * FROM dbo.GetCenterISOCodeEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterIsoCodeViewModel> GetVerifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterIsoCodeViewModel>("SELECT * FROM dbo.GetCenterISOCodeEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
