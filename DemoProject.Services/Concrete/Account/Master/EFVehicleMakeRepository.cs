using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.Concrete.Account.Master
{

    public class EFVehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IVehicleDbContextRepository vehicleDbContextRepository;


        public EFVehicleMakeRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository, IVehicleDbContextRepository _vehicleDbContextRepository)
        {
            context = _connection.EFDbContext;
            personDetailRepository = _personDetailRepository;
            vehicleDbContextRepository = _vehicleDbContextRepository;

        }

        public async Task<bool> Amend(VehicleMakeViewModel _vehicleMakeViewModel)
        {
            try
            {
                    bool result;
                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_vehicleMakeViewModel.VehicleMakeModificationPrmKey == 0)
                {
                    result = vehicleDbContextRepository.AttachVehicleMakeData(_vehicleMakeViewModel, StringLiteralValue.Amend);
                }
                else
                {
                    result = vehicleDbContextRepository.AttachVehicleMakeModificationData(_vehicleMakeViewModel, StringLiteralValue.Amend);

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

       
        public async Task<IEnumerable<VehicleMakeViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<VehicleMakeViewModel>("SELECT * FROM dbo.GetVehicleMakeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<VehicleMakeViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<VehicleMakeViewModel>("SELECT * FROM dbo.GetVehicleMakeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<VehicleMakeViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<VehicleMakeViewModel>("SELECT * FROM dbo.GetVehicleMakeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public bool GetUniqueVehicleMakeName(string _nameOfVehicleMake)
        {
            bool status;
            if (context.VehicleMakes.Where(p => p.NameOfVehicleMake == _nameOfVehicleMake).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }
        
        public async Task<VehicleMakeViewModel> GetEntry(Guid _vehicleMakeId, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<VehicleMakeViewModel>("SELECT * FROM dbo.GetVehicleMakeEntry (@VehicleMakeId, @EntriesType)", new SqlParameter("@VehicleMakeId", _vehicleMakeId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(VehicleMakeViewModel _vehicleMakeViewModel)
        {
            try
            {
                bool result = true;

                result = vehicleDbContextRepository.AttachVehicleMakeModificationData(_vehicleMakeViewModel, StringLiteralValue.Create);

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

       
        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(VehicleMakeViewModel _vehicleMakeViewModel)
        {
            try
            {

                bool result=true;

                result = vehicleDbContextRepository.AttachVehicleMakeData(_vehicleMakeViewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType)
        {
            try
            {
                bool result=true;

                if (_vehicleMakeViewModel.VehicleMakeModificationPrmKey > 0)
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                        // Modify Old Record 
                        VehicleMakeViewModel vehicleMakeViewModelForModify = await GetEntry(_vehicleMakeViewModel.VehicleMakeId, StringLiteralValue.Verify);
                        if(vehicleMakeViewModelForModify.IsModified ==true)
                        {
                            result = vehicleDbContextRepository.AttachVehicleMakeModificationData(vehicleMakeViewModelForModify, StringLiteralValue.Modify);
                        }
                        //else
                        //{
                        //    result = vehicleDbContextRepository.AttachVehicleMakeData(vehicleMakeViewModelForModify, StringLiteralValue.Modify);

                        //}
                        result = vehicleDbContextRepository.AttachVehicleMakeModificationData(_vehicleMakeViewModel, _entryType);

                    }
                    else
                    {
                        result = vehicleDbContextRepository.AttachVehicleMakeModificationData(_vehicleMakeViewModel, _entryType);
                    }
                }
                else
                {
                    if (_entryType == StringLiteralValue.Verify)
                    {
                        // Modify Old Record 
                        VehicleMakeViewModel vehicleMakeViewModelForModify = await GetEntry(_vehicleMakeViewModel.VehicleMakeId, StringLiteralValue.Verify);
                        if(vehicleMakeViewModelForModify != null)
                        {
                            result = vehicleDbContextRepository.AttachVehicleMakeData(vehicleMakeViewModelForModify, StringLiteralValue.Modify);
                        }
                    }
                        result = vehicleDbContextRepository.AttachVehicleMakeData(_vehicleMakeViewModel, _entryType);
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
