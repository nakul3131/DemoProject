using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterIndexViewModel
    {
        public short PrmKey { get; set; }
         
        public Guid CenterId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCenter { get; set; }
        
        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
