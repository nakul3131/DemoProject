using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.XtraPrinting.Native.Lines;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeEducationLoanParameter")]
    public partial class SchemeEducationLoanParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeEducationLoanParameter()
        {
            SchemeEducationLoanParameterMakerCheckers = new HashSet<SchemeEducationLoanParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool IsApplicableAllUniversities { get; set; }  
        
        public bool IsApplicableAllCourse { get; set; }   

        public decimal MinimumFees {  get; set; }   
      
        public decimal MaximumFees { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEducationLoanParameterMakerChecker> SchemeEducationLoanParameterMakerCheckers { get; set; }
    }
}
