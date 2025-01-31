using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ChequeBookMasterViewModel
    {
        public short PrmKey { get; set; }

        public Guid ChequeBookMasterId { get; set; }

        public int ChequeBookNumber { get; set; }

        public int FirstChequeNumber { get; set; }

        public short TotalNumberOfChequeLeaves { get; set; }

        public DateTime ChequeExpiryDate { get; set; }

        [StringLength(3)]
        public string Status { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short ChequeBookMasterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
