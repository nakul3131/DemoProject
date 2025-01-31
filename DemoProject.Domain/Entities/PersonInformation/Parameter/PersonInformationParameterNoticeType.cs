using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation.Parameter
{
    [Table("PersonInformationParameterNoticeType")]
    public partial class PersonInformationParameterNoticeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonInformationParameterNoticeType()
        {
            PersonInformationParameterNoticeTypeMakerCheckers = new HashSet<PersonInformationParameterNoticeTypeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        //public Guid PersonInformationParameterNoticeTypeId { get; set; }
        
        public byte PersonInformationParameterPrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public bool EnableNoticeInRegionalLanguage { get; set; }

        public byte MaximumResendsOnFailure { get; set; }

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

        public virtual NoticeType NoticeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInformationParameterNoticeTypeMakerChecker> PersonInformationParameterNoticeTypeMakerCheckers { get; set; }
    }
}
