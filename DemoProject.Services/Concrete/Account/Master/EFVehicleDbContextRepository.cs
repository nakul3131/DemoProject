using AutoMapper;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Wrapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Account.Master
{
   public class EFVehicleDbContextRepository : IVehicleDbContextRepository
   {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        readonly IAccountDetailRepository accountDetailRepository;
        // private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        //private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFVehicleDbContextRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository, IPersonDetailRepository _personDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            //enterpriseDetailRepository = _enterpriseDetailRepository;
            //managementDetailRepository = _managementDetailRepository;
            personDetailRepository = _personDetailRepository;
        }
        private EntityState entityState;
        private VehicleMake vehicleMake = new VehicleMake();
        private VehicleModel vehicleModel = new VehicleModel();
        public bool AttachVehicleMakeData(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_vehicleMakeViewModel, _entryType);
                _vehicleMakeViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_vehicleMakeViewModel.CenterId);

                // VehicleMake
                 vehicleMake = Mapper.Map<VehicleMake>(_vehicleMakeViewModel);
                VehicleMakeMakerChecker vehicleMakeMakerChecker = Mapper.Map<VehicleMakeMakerChecker>(_vehicleMakeViewModel);

                // VehicleMakeTranslation
                VehicleMakeTranslation vehicleMakeTranslation = Mapper.Map<VehicleMakeTranslation>(_vehicleMakeViewModel);
                VehicleMakeTranslationMakerChecker vehicleMakeTranslationMakerChecker = Mapper.Map<VehicleMakeTranslationMakerChecker>(_vehicleMakeViewModel);
                vehicleMake.PrmKey = _vehicleMakeViewModel.VehicleMakePrmKey;
                vehicleMakeTranslation.PrmKey = _vehicleMakeViewModel.VehicleMakeTranslationPrmKey;
                

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    vehicleMakeTranslation.PrmKey = _vehicleMakeViewModel.VehicleMakeTranslationPrmKey;
                    

                    context.VehicleMakes.Attach(vehicleMake);
                    context.Entry(vehicleMake).State = entityState;

                    context.VehicleMakeMakerCheckers.Attach(vehicleMakeMakerChecker);
                    context.Entry(vehicleMakeMakerChecker).State = EntityState.Added;
                    vehicleMake.VehicleMakeMakerCheckers.Add(vehicleMakeMakerChecker);

                    context.VehicleMakeTranslations.Attach(vehicleMakeTranslation);
                    context.Entry(vehicleMakeTranslation).State = entityState;
                    vehicleMake.VehicleMakeTranslations.Add(vehicleMakeTranslation);

                    context.VehicleMakeTranslationMakerCheckers.Attach(vehicleMakeTranslationMakerChecker);
                    context.Entry(vehicleMakeTranslationMakerChecker).State = EntityState.Added;
                    vehicleMakeTranslation.VehicleMakeTranslationMakerCheckers.Add(vehicleMakeTranslationMakerChecker);

                }
                else
                {
                    context.VehicleMakeMakerCheckers.Attach(vehicleMakeMakerChecker);
                    context.Entry(vehicleMakeMakerChecker).State = EntityState.Added;

                    context.VehicleMakeTranslationMakerCheckers.Attach(vehicleMakeTranslationMakerChecker);
                    context.Entry(vehicleMakeTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachVehicleMakeModificationData(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType)
        {
            try
            {
                short vehicleMakePrmkey = _vehicleMakeViewModel.VehicleMakePrmKey;
                configurationDetailRepository.SetDefaultValues(_vehicleMakeViewModel, _entryType);
                _vehicleMakeViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_vehicleMakeViewModel.CenterId);
                _vehicleMakeViewModel.VehicleMakePrmKey = vehicleMakePrmkey;
                
                // VehicleMakeModification
                VehicleMakeModification vehicleMakeModification = Mapper.Map<VehicleMakeModification>(_vehicleMakeViewModel);
                VehicleMakeModificationMakerChecker vehicleMakeModificationMakerChecker = Mapper.Map<VehicleMakeModificationMakerChecker>(_vehicleMakeViewModel);
                
                // VehicleMakeTranslation
                VehicleMakeTranslation vehicleMakeTranslation = Mapper.Map<VehicleMakeTranslation>(_vehicleMakeViewModel);
                VehicleMakeTranslationMakerChecker vehicleMakeTranslationMakerChecker = Mapper.Map<VehicleMakeTranslationMakerChecker>(_vehicleMakeViewModel);

                vehicleMakeTranslation.PrmKey = _vehicleMakeViewModel.VehicleMakeTranslationPrmKey;
                vehicleMakeModification.PrmKey = _vehicleMakeViewModel.VehicleMakeModificationPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.VehicleMakeModifications.Attach(vehicleMakeModification);
                    context.Entry(vehicleMakeModification).State = entityState;

                    context.VehicleMakeModificationMakerCheckers.Attach(vehicleMakeModificationMakerChecker);
                    context.Entry(vehicleMakeModificationMakerChecker).State = EntityState.Added;
                    vehicleMakeModification.VehicleMakeModificationMakerCheckers.Add(vehicleMakeModificationMakerChecker);

                    context.VehicleMakeTranslations.Attach(vehicleMakeTranslation);
                    context.Entry(vehicleMakeTranslation).State = entityState;
                    vehicleMake.VehicleMakeTranslations.Add(vehicleMakeTranslation);

                    context.VehicleMakeTranslationMakerCheckers.Attach(vehicleMakeTranslationMakerChecker);
                    context.Entry(vehicleMakeTranslationMakerChecker).State = EntityState.Added;
                    vehicleMakeTranslation.VehicleMakeTranslationMakerCheckers.Add(vehicleMakeTranslationMakerChecker);

                }
                else
                {
                    context.VehicleMakeModificationMakerCheckers.Attach(vehicleMakeModificationMakerChecker);
                    context.Entry(vehicleMakeModificationMakerChecker).State = EntityState.Added;

                    context.VehicleMakeTranslationMakerCheckers.Attach(vehicleMakeTranslationMakerChecker);
                    context.Entry(vehicleMakeTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachVehicleModelData(VehicleModelViewModel _vehicleModelViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_vehicleModelViewModel, _entryType);

                //VehicleModel
                VehicleModel vehicleModel = Mapper.Map<VehicleModel>(_vehicleModelViewModel);
                VehicleModelMakerChecker vehicleModelMakerChecker = Mapper.Map<VehicleModelMakerChecker>(_vehicleModelViewModel);

                //VehicleModelTranslation
                VehicleModelTranslation vehicleModelTranslation = Mapper.Map<VehicleModelTranslation>(_vehicleModelViewModel);
                VehicleModelTranslationMakerChecker vehicleModelTranslationMakerChecker = Mapper.Map<VehicleModelTranslationMakerChecker>(_vehicleModelViewModel);

                vehicleModel.VehicleMakePrmKey = accountDetailRepository.GetVehicleMakePrmKeyById(_vehicleModelViewModel.VehicleMakeId);
                vehicleModel.VehicleBodyTypePrmKey = accountDetailRepository.GetVehicleBodyTypePrmKeyById(_vehicleModelViewModel.VehicleBodyTypeId);

                vehicleModel.PrmKey = _vehicleModelViewModel.VehicleModelPrmKey;
                vehicleModelTranslation.PrmKey = _vehicleModelViewModel.VehicleModelTranslationPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    //VehicleModel
                    context.VehicleModels.Attach(vehicleModel);
                    context.Entry(vehicleModel).State = entityState;

                    context.VehicleModelMakerCheckers.Attach(vehicleModelMakerChecker);
                    context.Entry(vehicleModelMakerChecker).State = EntityState.Added;
                    vehicleModel.VehicleModelMakerCheckers.Add(vehicleModelMakerChecker);

                    //VehicleModelTranslation
                    context.VehicleModelTranslations.Attach(vehicleModelTranslation);
                    context.Entry(vehicleModelTranslation).State = entityState;
                    vehicleModel.VehicleModelTranslations.Add(vehicleModelTranslation);

                    context.VehicleModelTranslationMakerCheckers.Attach(vehicleModelTranslationMakerChecker);
                    context.Entry(vehicleModelTranslationMakerChecker).State = EntityState.Added;
                    vehicleModelTranslation.VehicleModelTranslationMakerCheckers.Add(vehicleModelTranslationMakerChecker);

                }
                else
                {
                    context.VehicleModelMakerCheckers.Attach(vehicleModelMakerChecker);
                    context.Entry(vehicleModelMakerChecker).State = EntityState.Added;

                    context.VehicleModelTranslationMakerCheckers.Attach(vehicleModelTranslationMakerChecker);
                    context.Entry(vehicleModelTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachVehicleModelModificationData(VehicleModelViewModel _vehicleModelViewModel, string _entryType)
        {
            try
            {
                short vehicleModelPrmkey = _vehicleModelViewModel.VehicleModelPrmKey;
                configurationDetailRepository.SetDefaultValues(_vehicleModelViewModel, _entryType);
                _vehicleModelViewModel.VehicleModelPrmKey = vehicleModelPrmkey;
                _vehicleModelViewModel.VehicleBodyTypePrmKey = accountDetailRepository.GetVehicleBodyTypePrmKeyById(_vehicleModelViewModel.VehicleBodyTypeId);

                //VehicleModelModification
                VehicleModelModification vehicleModelModification = Mapper.Map<VehicleModelModification>(_vehicleModelViewModel);
                VehicleModelModificationMakerChecker vehicleModelModificationMakerChecker = Mapper.Map<VehicleModelModificationMakerChecker>(_vehicleModelViewModel);

                //VehicleModelTranslation
                VehicleModelTranslation vehicleModelTranslation = Mapper.Map<VehicleModelTranslation>(_vehicleModelViewModel);
                VehicleModelTranslationMakerChecker vehicleModelTranslationMakerChecker = Mapper.Map<VehicleModelTranslationMakerChecker>(_vehicleModelViewModel);

                vehicleModel.VehicleMakePrmKey = accountDetailRepository.GetVehicleMakePrmKeyById(_vehicleModelViewModel.VehicleMakeId);
                vehicleModel.VehicleBodyTypePrmKey = accountDetailRepository.GetVehicleBodyTypePrmKeyById(_vehicleModelViewModel.VehicleBodyTypeId);

                vehicleModelTranslation.PrmKey = _vehicleModelViewModel.VehicleModelTranslationPrmKey;
                vehicleModelModification.PrmKey = _vehicleModelViewModel.VehicleModelModificationPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    // VehicleModel Modification 
                    context.VehicleModelModifications.Attach(vehicleModelModification);
                    context.Entry(vehicleModelModification).State = entityState;

                    context.VehicleModelModificationMakerCheckers.Attach(vehicleModelModificationMakerChecker);
                    context.Entry(vehicleModelModificationMakerChecker).State = EntityState.Added;
                    vehicleModelModification.VehicleModelModificationMakerCheckers.Add(vehicleModelModificationMakerChecker);


                    //VehicleModelTranslation
                    context.VehicleModelTranslations.Attach(vehicleModelTranslation);
                    context.Entry(vehicleModelTranslation).State = entityState;

                    context.VehicleModelTranslationMakerCheckers.Attach(vehicleModelTranslationMakerChecker);
                    context.Entry(vehicleModelTranslationMakerChecker).State = EntityState.Added;
                    vehicleModelTranslation.VehicleModelTranslationMakerCheckers.Add(vehicleModelTranslationMakerChecker);

                }
                else
                {
                    context.VehicleModelModificationMakerCheckers.Attach(vehicleModelModificationMakerChecker);
                    context.Entry(vehicleModelModificationMakerChecker).State = EntityState.Added;

                    context.VehicleModelTranslationMakerCheckers.Attach(vehicleModelTranslationMakerChecker);
                    context.Entry(vehicleModelTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

         public bool AttachVehicleVariantData(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_vehicleVariantViewModel, _entryType);

                //VehicleVariant
                VehicleVariant vehicleVariant = Mapper.Map<VehicleVariant>(_vehicleVariantViewModel);
                VehicleVariantMakerChecker vehicleVariantMakerChecker = Mapper.Map<VehicleVariantMakerChecker>(_vehicleVariantViewModel);

                //VehicleVariantTranslation
                VehicleVariantTranslation vehicleVariantTranslation = Mapper.Map<VehicleVariantTranslation>(_vehicleVariantViewModel);
                VehicleVariantTranslationMakerChecker vehicleVariantTranslationMakerChecker = Mapper.Map<VehicleVariantTranslationMakerChecker>(_vehicleVariantViewModel);
                vehicleVariant.VehicleModelPrmKey = accountDetailRepository.GetVehicleModelPrmKeyById(_vehicleVariantViewModel.VehicleModelId);

                vehicleVariant.PrmKey = _vehicleVariantViewModel.VehicleVariantPrmKey;
                vehicleVariantTranslation.PrmKey = _vehicleVariantViewModel.VehicleVariantTranslationPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    //VehicleVariant
                    context.VehicleVariants.Attach(vehicleVariant);
                    context.Entry(vehicleVariant).State = entityState;

                    context.VehicleVariantMakerCheckers.Attach(vehicleVariantMakerChecker);
                    context.Entry(vehicleVariantMakerChecker).State = EntityState.Added;
                    vehicleVariant.VehicleVariantMakerCheckers.Add(vehicleVariantMakerChecker);

                    //VehicleVariantTranslation
                    context.VehicleVariantTranslations.Attach(vehicleVariantTranslation);
                    context.Entry(vehicleVariantTranslation).State = entityState;
                    vehicleVariant.VehicleVariantTranslations.Add(vehicleVariantTranslation);

                    context.VehicleVariantTranslationMakerCheckers.Attach(vehicleVariantTranslationMakerChecker);
                    context.Entry(vehicleVariantTranslationMakerChecker).State = EntityState.Added;
                    vehicleVariantTranslation.VehicleVariantTranslationMakerCheckers.Add(vehicleVariantTranslationMakerChecker);


                }
                else
                {
                    context.Entry(vehicleVariantMakerChecker).State = EntityState.Added;
                    vehicleVariant.VehicleVariantMakerCheckers.Add(vehicleVariantMakerChecker);

                    context.VehicleVariantTranslationMakerCheckers.Attach(vehicleVariantTranslationMakerChecker);
                    context.Entry(vehicleVariantTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachVehicleVariantModificationData(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType)
        {
            try
            {
                short vehicleVariantPrmkey = _vehicleVariantViewModel.VehicleVariantPrmKey;
                configurationDetailRepository.SetDefaultValues(_vehicleVariantViewModel, _entryType);
                _vehicleVariantViewModel.VehicleVariantPrmKey = vehicleVariantPrmkey;
                //Mapping
                //VehicleVariantModification
                VehicleVariantModification vehicleVariantModification = Mapper.Map<VehicleVariantModification>(_vehicleVariantViewModel);
                VehicleVariantModificationMakerChecker vehicleVariantModificationMakerChecker = Mapper.Map<VehicleVariantModificationMakerChecker>(_vehicleVariantViewModel);

                //VehicleVariantTranslation
                VehicleVariantTranslation vehicleVariantTranslation = Mapper.Map<VehicleVariantTranslation>(_vehicleVariantViewModel);
                VehicleVariantTranslationMakerChecker vehicleVariantTranslationMakerChecker = Mapper.Map<VehicleVariantTranslationMakerChecker>(_vehicleVariantViewModel);

                vehicleVariantTranslation.PrmKey = _vehicleVariantViewModel.VehicleVariantTranslationPrmKey;
                vehicleVariantModification.PrmKey = _vehicleVariantViewModel.VehicleVariantModificationPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    //VehicleVariantModification
                    context.VehicleVariantModifications.Attach(vehicleVariantModification);
                    context.Entry(vehicleVariantModification).State = entityState;

                    context.VehicleVariantModificationMakerCheckers.Attach(vehicleVariantModificationMakerChecker);
                    context.Entry(vehicleVariantModificationMakerChecker).State = EntityState.Added;
                    vehicleVariantModification.VehicleVariantModificationMakerCheckers.Add(vehicleVariantModificationMakerChecker);

                    //VehicleVariantTranslation
                    context.VehicleVariantTranslations.Attach(vehicleVariantTranslation);
                    context.Entry(vehicleVariantTranslation).State = entityState;

                    context.VehicleVariantTranslationMakerCheckers.Attach(vehicleVariantTranslationMakerChecker);
                    context.Entry(vehicleVariantTranslationMakerChecker).State = EntityState.Added;
                    vehicleVariantTranslation.VehicleVariantTranslationMakerCheckers.Add(vehicleVariantTranslationMakerChecker);

                }
                else
                {
                    context.VehicleVariantModificationMakerCheckers.Attach(vehicleVariantModificationMakerChecker);
                    context.Entry(vehicleVariantModificationMakerChecker).State = EntityState.Added;

                    context.VehicleVariantTranslationMakerCheckers.Attach(vehicleVariantTranslationMakerChecker);
                    context.Entry(vehicleVariantTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public async Task<bool> SaveData()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
   }
}
