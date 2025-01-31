using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationIndexViewModel
    {
        public byte PrmKey { get; set; }

        public Guid OrganizationId { get; set; }

        [StringLength(100)]
        public string NameOfOrganization { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
