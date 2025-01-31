using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public  class SearchQueryViewModel
    {
        public Guid SearchQueryId { get; set; }

        [StringLength(1500)]
        public string QueryText { get; set; }

        public short SequenceNumber { get; set; }

    }
}
