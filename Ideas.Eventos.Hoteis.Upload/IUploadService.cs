using Ideas.Eventos.Hoteis.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Eventos.Hoteis.Upload
{
    public interface IUploadService
    {
        Task<List<string>> UploadImageCloudAzure(List<string> imageUrlList, List<string> imagePathList);
        Task<object> UploadFromUrl(FilterModel filter);
    }
    public class CloudBlobListImage
    {
        public MemoryStream Image { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
    }
}
