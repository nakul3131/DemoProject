using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Security.Users;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Pages = new HashSet<Page>();
            UserProfileMenus = new HashSet<UserProfileMenu>();
        }

        [Key]
        public int PrmKey { get; set; }

        [Required]        
        public Guid MenuId { get; set; }

        [Required]
        [StringLength(6)]
        public string MenuCode { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfMenu { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateName { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortcutCode { get; set; }

        public int SequenceNumber { get; set; }

        public int ParentMenuPrmKey { get; set; }

        [Required]
        [StringLength(200)]
        public string IconImageClass { get; set; }

        public bool IsVisible { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfController { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfActionMethod { get; set; }

        [Required]
        [StringLength(3)]
        public string TypeOfActionMethod { get; set; }

        public byte SecurityLevel { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Contents { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForClose { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Pages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileMenu> UserProfileMenus { get; set; }
    }
}
