using DemoProject.Domain.Entities.Account.Customer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAddress")]
    public partial class PersonAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAddress()
        {
            PersonAddressMakerCheckers = new HashSet<PersonAddressMakerChecker>();
            PersonAddressTranslations = new HashSet<PersonAddressTranslation>();
            CustomerAccountAddressDetails = new HashSet<CustomerAccountAddressDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAddressId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte AddressTypePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string FlatDoorNo { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBuilding { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfRoad { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfArea { get; set; }

        public short CityPrmKey { get; set; }

        public byte ResidenceTypePrmKey { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddressMakerChecker> PersonAddressMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddressTranslation> PersonAddressTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountAddressDetail> CustomerAccountAddressDetails { get; set; }
    }
}
