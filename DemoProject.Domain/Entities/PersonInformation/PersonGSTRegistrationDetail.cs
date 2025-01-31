using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonGSTRegistrationDetail")]
    public partial class PersonGSTRegistrationDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonGSTRegistrationDetail()
        {
            PersonGSTRegistrationDetailMakerCheckers = new HashSet<PersonGSTRegistrationDetailMakerChecker>();
            PersonGSTReturnDocuments = new HashSet<PersonGSTReturnDocument>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonGSTRegistrationDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public short StatePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GSTRegistrationTypePrmKey { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public byte GSTReturnPeriodicityPrmKey { get; set; }

        public bool IsApplicableEWayBill { get; set; }

        public decimal ThresholdLimit { get; set; }

        [Required]
        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? CloseDate { get; set; }

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
        public virtual ICollection<PersonGSTRegistrationDetailMakerChecker> PersonGSTRegistrationDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGSTReturnDocument> PersonGSTReturnDocuments { get; set; }
    }
}
