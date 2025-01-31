using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFEventMasterRepository : IEventMasterRepository
    {
        private readonly EFDbContext context;

        private readonly IManagementDetailRepository managementDetailRepository;

        public EFEventMasterRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;

            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Delete(Int16 prmKey)
        {
            try
            {
                var record = context.EventMasters.Where(a => a.PrmKey == prmKey).FirstOrDefault();

                if (record != null)
                {
                    context.EventMasters.Remove(record);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public List<EventMasterViewModel> GetEventMasterList()
        {
            return context.Database.SqlQuery<EventMasterViewModel>("SELECT * FROM dbo.GetEventMasterList()").ToList();
        }

        public List<EventMasterViewModel> GetEventTypeDropDown()
        {
            var eventTypes = context.EventTypes.Select(x => new EventMasterViewModel { PrmKey = x.PrmKey, NameOfEventType = x.NameOfEventType }).ToList();

            return eventTypes;
        }

        public async Task<bool> Save(EventMasterViewModel _eventMasterViewModel)
        {
            try
            {
                // Set Default Value
                //_eventMasterViewModel.EntryDateTime = DateTime.Now;
                _eventMasterViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                //_eventMasterViewModel.ReasonForModification = "None";
                //_eventMasterViewModel.Remark = "None";
                _eventMasterViewModel.TransReasonForModification = "None";
                //_eventMasterViewModel.UserAction = StringLiteralValue.Create;
                //_eventMasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                

                // Get PrmKey By Id
                //_eventMasterViewModel.EventTypePrmKey = eventTypeRepository.GetPrmKeyById(_eventMasterViewModel.EventTypeId);

                // Mapping
                // EventMaster
                EventMaster eventMaster = Mapper.Map<EventMaster>(_eventMasterViewModel);

                // EventMasterTranslation
                EventMasterTranslation eventMasterTranslation = Mapper.Map<EventMasterTranslation>(_eventMasterViewModel);

                if (_eventMasterViewModel.PrmKey > 0)
                {
                    context.EventMasters.Attach(eventMaster);
                    context.Entry(eventMaster).State = EntityState.Modified;

                    context.EventMasterTranslations.Attach(eventMasterTranslation);
                    context.Entry(eventMasterTranslation).State = EntityState.Modified;
                    eventMaster.EventMasterTranslations.Add(eventMasterTranslation);
                }
                else
                {
                    context.EventMasters.Attach(eventMaster);
                    context.Entry(eventMaster).State = EntityState.Added;

                    context.EventMasterTranslations.Attach(eventMasterTranslation);
                    context.Entry(eventMasterTranslation).State = EntityState.Added;
                    eventMaster.EventMasterTranslations.Add(eventMasterTranslation);
                }

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
