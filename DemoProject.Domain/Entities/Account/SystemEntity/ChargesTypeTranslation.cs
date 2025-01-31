using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("ChargesTypeTranslation")]
    public partial class ChargesTypeTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte ChargesTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string TransNameOfChargesType { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
    }
}
