using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeIndexViewModel
    {
        public int PrmKey { get; set; }

        public Guid EmployeeId { get; set; }

        [StringLength(100)]
        public string EmployeeCode { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }
    }
}
