using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("Group")]
    public partial class Group
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid GroupId { get; set; }

        public short GroupCategoryPrmKey { get; set; }

        public short GroupCreationPurposePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfGroup { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        public virtual PersonGroup PersonGroup { get; set; }
    }
}
