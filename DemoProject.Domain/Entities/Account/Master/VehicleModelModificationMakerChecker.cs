using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("VehicleModelModificationMakerChecker")]
    public partial class VehicleModelModificationMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short VehicleModelModificationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public string UserAction { get; set; }

        public string Remark { get; set; }

        public virtual VehicleModelModification VehicleModelModification { get; set; }
    }
}
