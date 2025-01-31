using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Conference;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("MeetingType")]
    public partial class MeetingType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingType()
        {
            MeetingTypeTranslations = new HashSet<MeetingTypeTranslation>();
            MeetingAllowances = new HashSet<MeetingAllowance>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid MeetingTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfMeetingType { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(3)]
        public string Invitees { get; set; }

        public byte MinimumNumberOfMeetings { get; set; }

        public short MaximumDaysBetweenMeetings { get; set; }

        public short MemberPresentyForQuorum { get; set; }

        public decimal MemberPresentyPercentageForQuorum { get; set; }

        public short BoardOfDirectorPresentyForQuorum { get; set; }

        [Required]
        [StringLength(3500)]
        public string NextReviewOfTermsOfReferenceDraft { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingTypeTranslation> MeetingTypeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowance> MeetingAllowances { get; set; }
    }
}
