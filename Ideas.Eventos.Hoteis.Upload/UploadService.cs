using Ideas.Eventos.Hoteis.Core.Interfaces;
using Ideas.Eventos.Hoteis.Core.Model;
using Ideas.Eventos.Hoteis.Core.Repository;
using Ideas.Eventos.Hoteis.Crawler;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Ideas.Eventos.Hoteis.Crawler.Model.Hotel;

namespace Ideas.Eventos.Hoteis.Upload
{
   public class UploadService : IUploadService
    {
        static string accountname = "homologaaodiag";
        static string key = "z8rbrdfxp1+VuHX2OOyLQ+Yv1H/PGOISB/MBxivHmYVtvpaibtRoe891jSeZ7w50OxjeuLIcxwtbbJ0ZWsh1Ag==";

        private IFullInfoRepository _repository;
        private static HotelService _service { get; set; }

        public UploadService(IFullInfoRepository repository)
        {
            _repository = repository;
        }

        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", accountname, key);
            return CloudStorageAccount.Parse(connectionString);
        }

        public async Task<List<string>> UploadImageCloudAzure(List<string> imageUrlList, List<string> imagePathList)
        {
            var listImageFullPath = new List<string>();

            var listCloudBlobListImage = new List<CloudBlobListImage>();
            int i = 0; 
            MemoryStream memoryStream = null;
            foreach(var img in imageUrlList) {

                WebClient client = new WebClient();
                using (memoryStream = new MemoryStream())
                {
                    Stream stream = client.OpenRead(img);
                    stream.CopyTo(memoryStream);

                    i++; 

                    listCloudBlobListImage.Add(new CloudBlobListImage
                    {
                        ContentType = "image/jpeg",
                        FilePath = imagePathList[i],
                        Image = memoryStream,
                    });

                    foreach (var file in listCloudBlobListImage)
                    {
                        if (file != null)
                        {
                            CloudBlockBlob blockBlob = GetPermissionCloudAzure(file.FilePath);

                            blockBlob.Properties.ContentType = file.ContentType;
                            // var ruiii = blockBlob.Parent.ListBlobs(true, BlobListingDetails.All, null, null);
                            await blockBlob.UploadFromStreamAsync(memoryStream);

                            //blockBlob.Properties.ContentTyape = file.ContentType;
                            //await blockBlob.UploadFromStreamAsync(file.Image);
                            listImageFullPath.Add(blockBlob.Uri.ToString());
                        }
                    }


                    stream.Flush();
                    stream.Close();
                    client.Dispose();
                }
            }  

            return listImageFullPath;
        }


        public async Task<object> UploadFromUrl(FilterModel filters)
        {
            var listHoteis = _repository.GetHoteis(filters); 


            foreach(var hotel in listHoteis) {  
                string imageFullPath = string.Empty; 

                var listCloudBlobListImage = new List<CloudBlobListImage>();
                int i = 0;
                MemoryStream memoryStream = null;
                foreach (var image in hotel.images)
                {

                    WebClient client = new WebClient();
                    using (memoryStream = new MemoryStream())
                    {
                        Stream stream = client.OpenRead(image.imageURL.original);
                        stream.CopyTo(memoryStream);
                        var imagePath = Path.GetFileName(image.imageURL.original);
                        i++;

                        listCloudBlobListImage.Add(new CloudBlobListImage
                        {
                            ContentType = "image/jpeg",
                            FilePath = string.Format("espaco-eventos/{0}/{1}", hotel.ofrgId, imagePath),
                            Image = memoryStream,
                        });

                        foreach (var file in listCloudBlobListImage)
                        {
                            if (file != null)
                            {
                                CloudBlockBlob blockBlob = GetPermissionCloudAzure(file.FilePath);

                                blockBlob.Properties.ContentType = file.ContentType;
                                // var ruiii = blockBlob.Parent.ListBlobs(true, BlobListingDetails.All, null, null);
                                await blockBlob.UploadFromStreamAsync(memoryStream);

                                //blockBlob.Properties.ContentTyape = file.ContentType;
                                //await blockBlob.UploadFromStreamAsync(file.Image);
                                imageFullPath = blockBlob.Uri.ToString();
                            }
                        }

                        hotel.images.Find(x => x.id == image.id).imageURL.original = imageFullPath;

                        var uptHotel = _repository.UpdateHotel(hotel.ofrgId, filters.Country, hotel);
                        stream.Flush();
                        stream.Close();
                        client.Dispose();
                    }
                }
            }


            return listHoteis; 
        }
        private CloudBlockBlob GetPermissionCloudAzure(string filePath)
        {
            CloudStorageAccount cloudStorageAccount = GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("ideasfractal");
            CloudBlockBlob BlockBlob = cloudBlobContainer.GetBlockBlobReference(filePath);

            return BlockBlob;
        }

    }
}
