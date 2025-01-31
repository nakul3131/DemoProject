using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation.Parameter
{
    [Table("PersonInformationParameterDocumentType")]
    public partial class PersonInformationParameterDocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonInformationParameterDocumentType()
        {
            PersonInformationParameterDocumentTypeMakerCheckers = new HashSet<PersonInformationParameterDocumentTypeMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        //public Guid PersonInformationParameterDocumentTypeId { get; set; }

        public byte PersonInformationParameterPrmKey { get; set; }

        public byte DocumentTypePrmKey { get; set; }
        
        public bool IsMandatory { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonInformationParameter PersonInformationParameter { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInformationParameterDocumentTypeMakerChecker> PersonInformationParameterDocumentTypeMakerCheckers { get; set; }
    }
}