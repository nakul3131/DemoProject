using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class VirtualAccountDetailViewModel
    {
        public int PrmKey { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public bool AccountStatus { get; set; }

        [StringLength(20)]
        public string CbsCustomerId { get; set; }

        [StringLength(20)]
        public string CustomerName { get; set; }

        [StringLength(20)]
        public string MemberId { get; set; }

        [StringLength(20)]
        public string CompanyId { get; set; }

        [StringLength(30)]
        public string ECollectionCode { get; set; }

        [StringLength(10)]
        public string MemberStatus { get; set; }

        [StringLength(20)]
        public string MobileNumber { get; set; }
    }
}
