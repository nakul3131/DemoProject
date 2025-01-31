using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountAddressDetailViewModel
    {

        public long PrmKey { get; set; }

        public Guid CustomerAccountAddressDetailId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonAddressPrmKey { get; set; }

        public byte AddressTypePrmKey { get; set; }

        [StringLength(50)]
        public string FlatDoorNo { get; set; }

        [StringLength(100)]
        public string NameOfBuilding { get; set; }

        [StringLength(100)]
        public string NameOfRoad { get; set; }

        [StringLength(100)]
        public string NameOfArea { get; set; }

        public short CityPrmKey { get; set; }

        public byte ResidenceTypePrmKey { get; set; }

        public byte ResidenceOwnershipPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonAddressTranslation

        public Guid PersonAddressTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransFlatDoorNo { get; set; }

        [StringLength(100)]
        public string TransNameOfBuilding { get; set; }

        [StringLength(100)]
        public string TransNameOfRoad { get; set; }

        [StringLength(100)]
        public string TransNameOfArea { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //Maker Checker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public long PersonAddressTranslationPrmKey { get; set; }

        //Person

        [StringLength(50)]
        public string FirstName { get; set; }

        // Translation In Regional

        // Other

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(100)]
        public string NameOfAddressType { get; set; }

        [StringLength(100)]
        public string NameOfCenter { get; set; }

        [StringLength(100)]
        public string NameOfResidenceType { get; set; }

        [StringLength(100)]
        public string NameOfOwnershipType { get; set; }

        // For SelectListItem

        public Guid PersonId { get; set; }

        public Guid AddressTypeId { get; set; }

        public Guid CityId { get; set; }

        public Guid ResidenceTypeId { get; set; }

        public Guid OwnershipTypeId { get; set; }

    }
}
