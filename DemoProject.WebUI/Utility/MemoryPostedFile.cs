using System.IO;
using System.Web;

namespace DemoProject.WebUI.Utility
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;
        private readonly string fileName;

        public MemoryPostedFile(byte[] fileBytes, string fileName)
        {
            this.fileBytes = fileBytes;
            this.fileName = fileName;
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName => fileName;

        public override Stream InputStream => new MemoryStream(fileBytes);

        public override void SaveAs(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                fileStream.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}