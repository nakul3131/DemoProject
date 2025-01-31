using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("EventTypeTranslation")]
    public partial class EventTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short EventTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfEventType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual EventType EventType { get; set; }
    }
}
