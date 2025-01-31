using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("EventMasterMakerChecker")]
    public partial class EventMasterMakerChecker
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short EventMasterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual EventMaster EventMaster { get; set; }
    }
}
