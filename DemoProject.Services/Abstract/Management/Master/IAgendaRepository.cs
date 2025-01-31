using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IAgendaRepository
    {
        // Amend Agenda Delete Entry - If Entry Rejected
        Task<bool> Amend(AgendaViewModel _agendaViewModel);

        int GetPrmKeyById(Guid _agendaId);

        // Droupdown List Values
        List<SelectListItem> AgendaDropdownList { get; }

        // Delete Agenda - Only For Rejected Entry
        Task<bool> Delete(AgendaViewModel _agendaViewModel);

        // Return Rejected Entries
        Task<IEnumerable<AgendaViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Agenda Table Which Are Not Authorized
        Task<IEnumerable<AgendaViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Agenda Table For Modification
        Task<IEnumerable<AgendaViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<AgendaViewModel> GetRejectedEntry(Guid _agendaId);

        // Return Record From Agenda Table By Given Parameter (i.e. AgendaId)
        Task<AgendaViewModel> GetUnVerifiedEntry(Guid _agendaId);

        // Return Record From Agenda Table By Given Parameter (i.e. AgendaId)
        Task<AgendaViewModel> GetVerifiedEntry(Guid _agendaId);

        bool GetUniqueAgendaName(string _nameOfAgenda);

        // Save Agenda Modification New Entry
        Task<bool> Modify(AgendaViewModel _agendaViewModel);

        // Reject Agenda Entry
        Task<bool> Reject(AgendaViewModel _agendaViewModel);

        // Save Agenda New Entry
        Task<bool> Save(AgendaViewModel _agendaViewModel);

        // Authorize Agenda Entry
        Task<bool> Verify(AgendaViewModel _agendaViewModel);
    }
}
