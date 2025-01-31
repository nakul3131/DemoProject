using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Servant
{
    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
            EmployeeDesignations = new HashSet<EmployeeDesignation>();
            EmployeeDetails = new HashSet<EmployeeDetail>();
            EmployeeDocuments = new HashSet<EmployeeDocument>();
            EmployeeMakerCheckers = new HashSet<EmployeeMakerChecker>();
            EmployeeModifications = new HashSet<EmployeeModification>();
            EmployeePerformanceRatings = new HashSet<EmployeePerformanceRating>();
            EmployeePhotoes = new HashSet<EmployeePhoto>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            EmployeeWorkingSchedules = new HashSet<EmployeeWorkingSchedule>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid EmployeeId { get; set; }

        [Required]
        [StringLength(30)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ExternalEmployeeId1 { get; set; }

        [Required]
        [StringLength(50)]
        public string ExternalEmployeeId2 { get; set; }

        [Required]
        [StringLength(3)]
        public string EmployeeCategory { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDesignation> EmployeeDesignations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDetail> EmployeeDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeMakerChecker> EmployeeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeModification> EmployeeModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePerformanceRating> EmployeePerformanceRatings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePhoto> EmployeePhotoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeWorkingSchedule> EmployeeWorkingSchedules { get; set; }
    }
}
