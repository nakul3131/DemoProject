using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IEventMasterRepository
    {
        // Delete EventMaster - Only For Rejected Entry
        Task<bool> Delete(Int16 prmKey);

        //List<SelectListItem> EventMasterDropdownList { get; }

        List<EventMasterViewModel> GetEventMasterList();

        List<EventMasterViewModel> GetEventTypeDropDown();

        // Save EventMaster New Entry
        Task<bool> Save(EventMasterViewModel _eventMasterViewModel);

    }
}
