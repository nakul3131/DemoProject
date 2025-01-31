using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonPrefix")]
    public partial class PersonPrefix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonPrefix()
        {
            PersonPrefixMakerCheckers = new HashSet<PersonPrefixMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PrmKey { get; set; }

        //public Guid PersonPrefixId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte PrefixPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

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

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual Person Person { get; set; }

        public virtual Prefix Prefix { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonPrefixMakerChecker> PersonPrefixMakerCheckers { get; set; }
    }
}
