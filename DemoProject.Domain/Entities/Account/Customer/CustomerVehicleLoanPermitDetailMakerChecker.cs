﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerVehicleLoanPermitDetailMakerChecker")]
    public partial class CustomerVehicleLoanPermitDetailMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanPermitDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual CustomerVehicleLoanPermitDetail CustomerVehicleLoanPermitDetail { get; set; }

    }
}
