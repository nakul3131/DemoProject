using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonIndexViewModel
    {
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }
        
        [StringLength(500)]
        public string FullName { get; set; }

        public DateTime EntryDateTime { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }
    }
}
