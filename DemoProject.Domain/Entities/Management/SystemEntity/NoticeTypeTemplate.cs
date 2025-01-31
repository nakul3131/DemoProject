using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("NoticeTypeTemplate")]
    public partial class NoticeTypeTemplate
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeTypeTemplateId { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public short DLTTemplatePrmKey { get; set; }

        public byte HeaderPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual NoticeType NoticeType { get; set; }
    }
}
