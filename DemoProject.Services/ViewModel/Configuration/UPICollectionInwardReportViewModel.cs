using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class UPICollectionInwardReportViewModel
    {
        public bool IsIncremental { get; set; }

        public DateTime PayDate { get; set; }

        [StringLength(500)]
        public string VirtualPaymentAddress { get; set; }

        [StringLength(1500)]
        public string Filler1 { get; set; }

        [StringLength(1500)]
        public string Filler2 { get; set; }
    }
}
