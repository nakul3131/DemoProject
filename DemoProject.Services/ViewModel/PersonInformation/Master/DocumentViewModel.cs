using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class DocumentViewModel
    {
        // Document
        public long MinimumFileSize { get; set; }

        public long MaximumFileSize { get; set; }

        [StringLength(1500)]
        public string AllowedFileFormats { get; set; }

        public string[] AllowedFileFormatsExt { get; set; }
    }
}
