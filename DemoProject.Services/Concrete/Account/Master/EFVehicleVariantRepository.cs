using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFVehicleVariantRepository : IVehicleVariantRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IVehicleDbContextRepository vehicleDbContextRepository;

        public EFVehicleVariantRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository, IAccountDetailRepository _accountDetailRepository, IVehicleDbContextRepository _vehicleDbContextRepository)
        {
            context = _connection.EFDbContext;
            personDetailRepository = _personDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            vehicleDbContextRepository = _vehicleDbContextRepository;
        }

        public async Task<bool> Amend(VehicleVariantViewModel _vehicleVariantViewModel)
        {
            try
            {
                bool result = true;
                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_vehicleVariantViewModel.VehicleVariantModificationPrmKey == 0)
                {
                    result = vehicleDbContextRepository.AttachVehicleVariantData(_vehicleVariantViewModel, StringLiteralValue.Amend);
                }
                else
                {
                    result = vehicleDbContextRepository.AttachVehicleVariantModificationData(_vehicleVariantViewModel, StringLiteralValue.Amend);

                }

                if (result)
                {
                    result = await vehicleDbContextRepository.SaveData();
                }

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<VehicleVariantViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<VehicleVariantViewModel>("SELECT * FROM dbo.GetVehicleVariantEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<VehicleVariantViewModel> GetEntry(Guid _vehicleVariantId,string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<VehicleVariantViewModel>("SELECT * FROM dbo.GetVehicleVariantEntry (@VehicleVariantId, @EntriesType)", new SqlParameter("@VehicleVariantId", _vehicleVariantId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(VehicleVariantViewModel _vehicleVariantViewModel)
        {
            try
            {
                bool result = true;

                result = vehicleDbContextRepository.AttachVehicleVariantModificationData(_vehicleVariantViewModel, StringLiteralValue.Create);

                if (result)
                {
                    result = await vehicleDbContextRepository.SaveData();
                }

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(VehicleVariantViewModel _vehicleVariantViewModel)
        {
            try
            {
                bool result = true;

                result = vehicleDbContextRepository.AttachVehicleVariantData(_vehicleVariantViewModel, StringLiteralValue.Create);

                if (result)
                {
                    result = await vehicleDbContextRepository.SaveData();
                }

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType)
        {
            try
            {
                bool result;
                if (_vehicleVariantViewModel.VehicleVariantModificationPrmKey > 0)
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                            // Modify Old Record 
                       VehicleVariantViewModel vehicleVariantViewModelForModify = await GetEntry(_vehicleVariantViewModel.VehicleVariantId,StringLiteralValue.Verify);
                        if (vehicleVariantViewModelForModify.IsModified == true)
                        {
                            result = vehicleDbContextRepository.AttachVehicleVariantModificationData(vehicleVariantViewModelForModify, StringLiteralValue.Modify);
                        }
                    }
                        result = vehicleDbContextRepository.AttachVehicleVariantModificationData(_vehicleVariantViewModel, _entryType);
                }
                else
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                        // Modify Old Record 
                        VehicleVariantViewModel vehicleVariantViewModelForModify = await GetEntry(_vehicleVariantViewModel.VehicleModelId, StringLiteralValue.Verify);
                        if (vehicleVariantViewModelForModify != null)
                        {
                            result = vehicleDbContextRepository.AttachVehicleVariantData(vehicleVariantViewModelForModify, StringLiteralValue.Modify);
                        }
                    }
                    result = vehicleDbContextRepository.AttachVehicleVariantData(_vehicleVariantViewModel, _entryType);
                }

                if (result)
                {
                    result = await vehicleDbContextRepository.SaveData();
                }

                if (result)
                    return true;
                else
                    return false;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

    }
}
