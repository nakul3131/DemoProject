using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class BankIdDetailViewModel
    {
        public byte PrmKey { get; set; }

        [StringLength(11)]
        public string IFSCCode { get; set; } = "ICIC0001409";

        [StringLength(50)]
        public string AccountNumber { get; set; } = "140905500126";
    }
}
