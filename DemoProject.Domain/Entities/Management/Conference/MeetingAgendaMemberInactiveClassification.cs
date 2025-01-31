using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingAgendaMemberInactiveClassification")]
    public partial class MeetingAgendaMemberInactiveClassification
    {
        [Key]
        public int PrmKey { get; set; }

        public int MeetingAgendaPrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime ClassificationDate { get; set; }
    }
}
