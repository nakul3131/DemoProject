using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("AppLanguage")]
    public partial class AppLanguage
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid AppLanguageId { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        public string NameOfAppLanguage { get; set; }

        [Required]
        [StringLength(20)]
        public string NameInUnicode { get; set; }

        [Required]
        [StringLength(20)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(400)]
        public string Months { get; set; }

        [Required]
        [StringLength(150)]
        public string ShortMonths { get; set; }

        [Required]
        [StringLength(250)]
        public string WeekDays { get; set; }

        [Required]
        [StringLength(250)]
        public string ShortWeekDays { get; set; }

        [Required]
        [StringLength(100)]
        public string Numbers { get; set; }
    }
}
