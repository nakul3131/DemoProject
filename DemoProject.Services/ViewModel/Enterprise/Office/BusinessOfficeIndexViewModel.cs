using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeIndexViewModel
    {
        public short PrmKey { get; set; }

        public Guid BusinessOfficeId { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
