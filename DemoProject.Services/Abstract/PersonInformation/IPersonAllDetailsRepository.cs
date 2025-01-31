using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.PersonInformation
{
     public interface IPersonAllDetailsRepository
    {
        List<SelectListItem> Relatives { get; }

        List<SelectListItem> AddressTypess { get; }

        List<SelectListItem> CastCategories { get; }

        List<SelectListItem> Cities { get; }

        List<SelectListItem> EducationalQualifications { get; }

        List<SelectListItem> HomeBranch { get; }

        List<SelectListItem> Groups { get; }

        List<SelectListItem> Occupations { get; }

        List<SelectListItem> Designations { get; }

        List<SelectListItem> BankBranches { get; }

        List<SelectListItem> VechicleModels { get; }

        List<SelectListItem> VechicleVariants { get; }

        List<SelectListItem> CustomFields { get; }

        List<SelectListItem> Identifications { get; }

        List<SelectListItem> Personlist { get; }

        //// Amend Person Delete Entry - If Entry Rejected
        //Task<bool> Amend(PersonViewModel _personViewModel);

        //// Amend Person Modification Entry - If Entry Rejected
        //Task<bool> AmendModification(PersonViewModel _personViewModel);

        //// Delete Person - Only For Rejected Entry
        //Task<bool> Delete(PersonViewModel _personViewModel);

        //// Delete Person Modification Entry - Only For Rejected Entry
        //Task<bool> DeleteModification(PersonViewModel _personViewModel);

        //// Return Rejected Entries
        //Task<IEnumerable<PersonViewModel>> GetIndexOfRejectedEntries();

        //// Return Modification Rejected Entries
        //Task<IEnumerable<PersonViewModel>> GetIndexOfRejectedEntriesFromModification();

        //// Return Valid List From Person Table Which Are Not Authorized
        //Task<IEnumerable<PersonViewModel>> GetIndexOfUnVerifiedEntries();

        //// Return Modification UnAuthorized Entries
        //Task<IEnumerable<PersonViewModel>> GetIndexOfUnVerifiedEntriesFromModification();

        //// Return Valid List From Person Table For Modification
        //Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntries();

        //// Return Valid List From Person Table For Modification
        //Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntriesFromModification();

        //// Return Modification Rejected Entry
        //Task<PersonViewModel> GetModificationRejectedEntry(Guid _personId);

        //// Return Modification UnAuthorized Entry
        //Task<PersonViewModel> GetModificationUnVerifiedEntry(Guid _personId);

        //// Return Record From Person Table By Given Parameter (i.e. PersonId)
        //Task<PersonViewModel> GetModificationVerifiedEntry(Guid _personId);

        //// Return Rejected Entry
        //Task<PersonViewModel> GetRejectedEntry(Guid _personId);

        //// Return Record From Person Table By Given Parameter (i.e. PersonId)
        //Task<PersonViewModel> GetUnVerifiedEntry(Guid _personId);

        //// Return Record From Person Table By Given Parameter (i.e. PersonId)
        //Task<PersonViewModel> GetVerifiedEntry(Guid _personId);

        //// Reject Person Entry
        //Task<bool> Reject(PersonViewModel _personViewModel);

        //// Reject Person Modification Entry
        //Task<bool> RejectModification(PersonViewModel _personViewModel);

        //// Save Person New Entry
        //Task<bool> Save(PersonViewModel _personViewModel);

        //// Save Person Modification New Entry
        //Task<bool> SaveModification(PersonViewModel _personViewModel);

        //// Authorize Person Entry
        //Task<bool> Verify(PersonViewModel _personViewModel);

        //// Authorize Person Modification Entry
        //Task<bool> VerifyModification(PersonViewModel _personViewModel);
    }
}
