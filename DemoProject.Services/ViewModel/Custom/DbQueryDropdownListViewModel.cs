using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Custom
{
    public class DbQueryDropdownListViewModel
    {
        public Guid ValueId { get; set; }

        [StringLength(500)]
        public string ValueText { get; set; }
    }
}
