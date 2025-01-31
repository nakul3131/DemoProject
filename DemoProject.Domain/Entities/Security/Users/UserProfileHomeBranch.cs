using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileHomeBranch")]
    public partial class UserProfileHomeBranch
    {
        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short BranchPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }
    }
}
