using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFVehicleModelRepository : IVehicleModelRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IVehicleDbContextRepository vehicleDbContextRepository;

        public EFVehicleModelRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository, IAccountDetailRepository _accountDetailRepository, IVehicleDbContextRepository _vehicleDbContextRepository)
        {
            context = _connection.EFDbContext;
            personDetailRepository = _personDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            vehicleDbContextRepository = _vehicleDbContextRepository;
        }

        public async Task<bool> Amend(VehicleModelViewModel _vehicleModelViewModel)
        {
            try
            {
                bool result=true;
                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_vehicleModelViewModel.VehicleModelModificationPrmKey == 0)
                {
                    result = vehicleDbContextRepository.AttachVehicleModelData(_vehicleModelViewModel, StringLiteralValue.Amend);
                }
                else
                {
                    result = vehicleDbContextRepository.AttachVehicleModelModificationData(_vehicleModelViewModel, StringLiteralValue.Amend);

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

        public async Task<IEnumerable<VehicleModelViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<VehicleModelViewModel>("SELECT * FROM dbo.GetVehicleModelEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<VehicleModelViewModel> GetEntry(Guid _vehicleModelId , string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<VehicleModelViewModel>("SELECT * FROM dbo.GetVehicleModelEntry (@VehicleModelId, @EntriesType)", new SqlParameter("@VehicleModelId", _vehicleModelId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(VehicleModelViewModel _vehicleModelViewModel)
        {
            try
            {
                bool result = true;

                result = vehicleDbContextRepository.AttachVehicleModelModificationData(_vehicleModelViewModel, StringLiteralValue.Create);

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


        public async Task<bool> Save(VehicleModelViewModel _vehicleModelViewModel)
        {
            try
            {
                bool result = true;

                result = vehicleDbContextRepository.AttachVehicleModelData(_vehicleModelViewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(VehicleModelViewModel _vehicleModelViewModel,string _entryType)
        {
            try
            {
                bool result;
                if (_vehicleModelViewModel.VehicleModelModificationPrmKey > 0)
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                        // Modify Old Record 
                        VehicleModelViewModel vehicleModelViewModelForModify = await GetEntry(_vehicleModelViewModel.VehicleModelId ,StringLiteralValue.Verify);
                        if (vehicleModelViewModelForModify.IsModified == true)
                        {
                            result = vehicleDbContextRepository.AttachVehicleModelModificationData(vehicleModelViewModelForModify, StringLiteralValue.Modify);
                        }
                    }
                        result = vehicleDbContextRepository.AttachVehicleModelModificationData(_vehicleModelViewModel, _entryType);
                    
                }
                else
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                        // Modify Old Record 
                        VehicleModelViewModel vehicleModelViewModelForModify = await GetEntry(_vehicleModelViewModel.VehicleModelId, StringLiteralValue.Verify);
                        if (vehicleModelViewModelForModify != null)
                        {
                            result = vehicleDbContextRepository.AttachVehicleModelData(vehicleModelViewModelForModify, StringLiteralValue.Modify);
                        }
                    }
                    result = vehicleDbContextRepository.AttachVehicleModelData(_vehicleModelViewModel, _entryType);
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
