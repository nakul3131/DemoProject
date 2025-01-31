using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("VehicleSupplier")]
    public partial class VehicleSupplier
    {
        [Key]
        public int PrmKey { get; set; }

        public Guid VehicleSupplierId { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
