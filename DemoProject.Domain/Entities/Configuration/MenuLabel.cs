using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("MenuLabel")]
    public partial class MenuLabel
    {
        [Key]

        public int PrmKey { get; set; }

        public int MenuPrmKey { get; set; }

        public int MLWordPrmKey { get; set; }
    }
}
