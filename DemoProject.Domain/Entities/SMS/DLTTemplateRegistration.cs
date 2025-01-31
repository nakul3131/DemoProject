using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("DLTTemplateRegistration")]
    public partial class DLTTemplateRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DLTTemplateRegistration()
        {
            DLTTemplateRegistrationMakerCheckers = new HashSet<DLTTemplateRegistrationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        [Required]
        public Guid DLTTemplateRegistrationId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfTemplate { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(50)]
        public string TemplateId { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string TemplateContent { get; set; }

        [Required]
        [StringLength(1500)]
        public string TemplateContentWithParameterInfo { get; set; }

        [Required]
        [StringLength(1500)]
        public string TemplateContentUnicode { get; set; }

        [Required]
        [StringLength(1500)]
        public string TemplateContentUnicodeWithParameterInfo { get; set; }

        [Required]
        [StringLength(500)]
        public string Headers { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual Language Language { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DLTTemplateRegistrationMakerChecker> DLTTemplateRegistrationMakerCheckers { get; set; }
    }
}
