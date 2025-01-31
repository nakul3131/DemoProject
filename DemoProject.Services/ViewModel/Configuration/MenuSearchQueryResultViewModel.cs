using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class MenuSearchQueryResultViewModel
    {
        public int PrmKey { get; set; }

        [StringLength(1500)]
        public string ResultUrl { get; set; }

        [StringLength(1500)]
        public string ShortDescription { get; set; }

        [StringLength(50)]
        public string NameOfController { get; set; }

        [StringLength(50)]
        public string NameOfActionMethod { get; set; }


    }
}
