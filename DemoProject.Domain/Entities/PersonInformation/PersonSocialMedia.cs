using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonSocialMedia")]
    public partial class PersonSocialMedia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonSocialMedia()
        {
            PersonSocialMediaMakerCheckers = new HashSet<PersonSocialMediaMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonSocialMediaId { get; set; }

        public long PersonPrmKey { get; set; }

        public short SocialMediaPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string SocialMediaLink { get; set; }

        [Required]
        [StringLength(2500)]
        public string OtherDetails { get; set; }

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
        public virtual ICollection<PersonSocialMediaMakerChecker> PersonSocialMediaMakerCheckers { get; set; }
    }
}
