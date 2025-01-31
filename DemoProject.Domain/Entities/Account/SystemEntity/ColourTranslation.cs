using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("ColourTranslation")]
    public partial class ColourTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short ColourPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNameOfColour { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual Colour Colour { get; set; }
    }
}
