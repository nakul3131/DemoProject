using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.FinancialStatement
{
    [Table("FinancialYear")]
    public class FinancialYear
    {
        [Key]
        public byte PrmKey { get; set; }

        public string NameOfFinancialYear { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsClosed { get; set; }

        public bool IsCurrent { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }
    }
}
