using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("CustomerTypeTranslation")]
    public partial class CustomerTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short CustomerTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCustomerType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerType CustomerType { get; set; }

        public virtual Language Language { get; set; }
    }
}
